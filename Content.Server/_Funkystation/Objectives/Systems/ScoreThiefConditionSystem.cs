using Content.Server._Funkystation.Objectives.Components;
using Content.Server.Cargo.Systems;
using Content.Shared.Interaction;
using Content.Shared.Objectives.Components;
using Content.Shared.Objectives.Systems;
using Robust.Shared.Containers;
using Robust.Shared.Random;
using Robust.Shared.Utility;

namespace Content.Server._Funkystation.Objectives.Systems;

/// <summary>
/// The objective system for scorethief
/// </summary>
// Large portions of this were taken from https://github.com/funky-station/forky-station/blob/22a547c7c8aa1f0f6ac5f8c3e9941f2dfc25bd17/Content.Server/Objectives/Systems/StealConditionSystem.cs
public sealed partial class ScoreThiefConditionSystem : EntitySystem
{
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private MetaDataSystem _metaData = default!;
    [Dependency] private SharedObjectivesSystem _objectives = default!;
    [Dependency] private EntityLookupSystem _lookup = default!;
    [Dependency] private SharedInteractionSystem _interaction = default!;
    [Dependency] private IEntityManager _entManager = default!;
    [Dependency] private EntityQuery<ContainerManagerComponent> _containerQuery = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ScoreThiefConditionComponent, ObjectiveAssignedEvent>(OnAssigned);
        SubscribeLocalEvent<ScoreThiefConditionComponent, ObjectiveAfterAssignEvent>(OnAfterAssign);
        SubscribeLocalEvent<ScoreThiefConditionComponent, ObjectiveGetProgressEvent>(OnGetProgress);

    }

    private void OnAssigned(Entity<ScoreThiefConditionComponent> condition, ref ObjectiveAssignedEvent args)
    {
        condition.Comp.TargetScore = _random.Next(
            (condition.Comp.AmountSpesos - condition.Comp.AmountSpesosVariance)/condition.Comp.AmountSpesosVarianceInterval,
            (condition.Comp.AmountSpesos + condition.Comp.AmountSpesosVariance)/condition.Comp.AmountSpesosVarianceInterval
            ) * condition.Comp.AmountSpesosVarianceInterval;
    }

    private void OnAfterAssign(Entity<ScoreThiefConditionComponent> condition, ref ObjectiveAfterAssignEvent args)
    {
        string rsiState;
        switch(condition.Comp.TargetScore)
        {
            case 1:
                rsiState = "cash";
                break;
            case <= 10:
                rsiState = "cash_10";
                break;
            case <= 100:
                rsiState = "cash_100";
                break;
            case <= 500:
                rsiState = "cash_500";
                break;
            case <= 1000:
                rsiState = "cash_1000";
                break;
            case <= 5000:
                rsiState = "cash_5000";
                break;
            case <= 10000:
                rsiState = "cash_10000";
                break;
            case <= 25000:
                rsiState = "cash_25000";
                break;
            case <= 50000:
                rsiState = "cash_50000";
                break;
            case <= 100000:
                rsiState = "cash_100000";
                break;
            default:
                rsiState = "cash_1000000";
                break;
        }
        var sprite = new SpriteSpecifier.Rsi(new ResPath("/Textures/Objects/Economy/cash.rsi"), rsiState);

        _metaData.SetEntityName(condition.Owner,
            Loc.GetString("scorethief-objective-title-one") + condition.Comp.TargetScore + Loc.GetString("scorethief-objective-title-two"),
            args.Meta);
        _objectives.SetIcon(condition.Owner, sprite, args.Objective);
    }

    private void OnGetProgress(Entity<ScoreThiefConditionComponent> condition, ref ObjectiveGetProgressEvent args)
    {
        _metaData.SetEntityDescription(condition.Owner, (int)((float)condition.Comp.CurrentScore/condition.Comp.TargetScore*100) + "%");
        if (!_containerQuery.TryGetComponent(args.MindId, out var currentManager))
            return;

        var containerStack = new Stack<ContainerManagerComponent>();
        var priceSystem = _entManager.System<PricingSystem>();
        condition.Comp.CurrentScore = 0;

        // Check steal areas
        if (condition.Comp.CheckStealAreas)
        {
            var areasQuery = AllEntityQuery<StealAreaComponent, TransformComponent>();
            while (areasQuery.MoveNext(out var uid, out var area, out var xform))
            {
                if (!area.Owners.Contains(args.MindId))
                    continue;

                HashSet<Entity<TransformComponent>> nearestEnts = new();

                _lookup.GetEntitiesInRange<TransformComponent>(xform.Coordinates, area.Range, nearestEnts);
                foreach (var ent in nearestEnts)
                {
                    if (!_interaction.InRangeUnobstructed((uid, xform), (ent, ent.Comp), range: area.Range))
                        continue;

                    //TODO: use ScoreThiefPriceModifierComponent
                    condition.Comp.CurrentScore += (int)priceSystem.GetPrice(ent, false);

                    //If it's a container, check it later
                    if (_containerQuery.TryGetComponent(ent, out var containerManager))
                    {
                        containerStack.Push(containerManager);
                    }
                }
            }
        }

        // recursively check each container
        // checks inventory, bag, implants, etc.
        //TODO: use ScoreThiefPriceModifierComponent
        do
        {
            foreach (var container in currentManager.Containers.Values)
            {
                foreach (var entity in container.ContainedEntities)
                {
                    condition.Comp.CurrentScore += (int)priceSystem.GetPrice(entity, false);

                    // if it's a container check its contents
                    if (_containerQuery.TryGetComponent(entity, out var containerManager))
                        containerStack.Push(containerManager);
                }
            }
        } while (containerStack.TryPop(out currentManager));

        args.Progress = Math.Clamp((float)condition.Comp.CurrentScore / condition.Comp.TargetScore, 0f, 1f);
    }
}
