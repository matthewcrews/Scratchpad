// module Client.View

// open System
// open Elmish
// open Feliz
// open Feliz.Bulma
// open Feliz.ReactFlow
// open DiscreteRate.Types
// open DiscreteRate.Types.Summary
// open Shared
// open Client.Types
// open Fable.Core.JsInterop
// open Shared.Playback
// open Fable.Core
// open Feliz.Plotly
// importAll "./Styles.css"

// [<ReactComponent>]
// let Valve (props: {| data : Valve |}) =
//     let (ValveId name) = props.data.Id
//     let valveImage =
//         match props.data.Status with
//         | ValveStatus.Satisfied -> "valve/valve_FullFlow.gif"
//         | ValveStatus.BlockedBy _ -> "valve/valve_Blocked.gif"
//         | ValveStatus.StarvedBy _ -> "valve/valve_Starved.gif"
//         | ValveStatus.BlockedAndStarvedBy _ -> "valve/valve_BlockedStarved.gif"
//         | ValveStatus.FullyBlockedBy _ -> "valve/valve_FullyBlocked.gif"
//         | ValveStatus.FullyStarvedBy _ -> "valve/valve_FullyStarved.gif"
//         | ValveStatus.FullyBlockedAndStarvedBy _ -> "valve/valve_FullyBlockedStarved.gif"

//     Html.div [
//         prop.children [
//             ReactFlow.handle [
//                 handle.``type`` Target
//                 handle.position Left
//             ]
//             ReactFlow.handle [
//                 handle.``type`` Source
//                 handle.position Right
//             ]
//             Html.div [
//                 prop.style [
//                     style.display.flex
//                     style.justifyContent.center
//                 ]
//                 prop.text name
//             ]
//             Html.div [
//                 prop.style [
//                     style.display.flex
//                     style.justifyContent.center
//                 ]
//                 prop.children [
//                     Html.img [
//                         prop.style [ style.alignContent.center ]
//                         prop.src valveImage
//                     ]
//                 ]
//             ]
//         ]
//     ]

// [<ReactComponent>]
// let Split (props: {| data : Split |}) =
//     let (SplitId name) = props.data.Id
//     let splitImage = "split/split.png"

//     Html.div [
//         prop.children [
//             ReactFlow.handle [
//                 handle.``type`` Target
//                 handle.position Left
//             ]
//             ReactFlow.handle [
//                 handle.``type`` Source
//                 handle.position Right
//             ]
//             Html.div [
//                 prop.style [
//                     style.display.flex
//                     style.justifyContent.center
//                 ]
//                 prop.text name
//             ]
//             Html.div [
//                 prop.style [
//                     style.display.flex
//                     style.justifyContent.center
//                 ]
//                 prop.children [
//                     Html.img [
//                         prop.style [ style.alignContent.center ]
//                         prop.src splitImage
//                     ]
//                 ]
//             ]
//         ]
//     ]


// [<ReactComponent>]
// let Tank (props: {| data : Tank |}) =
//     let (TankId name) = props.data.Id
//     let fileIndex = int (10.0 * (props.data.Level / props.data.Capacity))
//     let tankFile = $"animated_tank/animated_tank_Level{fileIndex}.gif"
//     //Popover.popover [
//     //let isSelect = props.data.
//     Html.div [

//         prop.children [
//             ReactFlow.handle [
//                 handle.``type`` Target
//                 handle.position Left
//             ]
//             ReactFlow.handle [
//                 handle.``type`` Source
//                 handle.position Right
//             ]
//             Html.div [
//                 prop.style [
//                     style.display.flex
//                     style.justifyContent.center
//                 ]
//                 prop.text name
//             ]
//             Html.div [
//                 prop.style [
//                     style.display.flex
//                     style.justifyContent.center
//                 ]
//                 prop.children [
//                     Html.img [
//                         prop.style [ style.alignContent.center ]
//                         prop.src tankFile
//                     ]
//                 ]
//             ]
//         ]
//     ]

//         //Popover.content [
//         //    Html.div "Chicken!"
//         //]
//     //]

// let navBrand =
//     Bulma.navbarBrand.div [
//         Bulma.navbarItem.a [
//             prop.href "https://safe-stack.github.io/"
//             navbarItem.isActive
//             prop.children [
//                 Html.img [
//                     prop.src "/favicon.png"
//                     prop.alt "Logo"
//                 ]
//             ]
//         ]
//     ]


// let tankNode (n: Shared.Layout.Node) (tank: Tank) =
//     Feliz.ReactFlow.ReactFlow.node [
//         node.id (n.Id.ToString())
//         node.nodetype (Custom "tank")
//         node.data tank
//         node.position (n.XCoord, n.YCoord)
//         node.style [
//             style.width 50
//         ]
//     ]


// let valveNode (n: Shared.Layout.Node) (valve: Valve) =
//     ReactFlow.node [
//         node.id (n.Id.ToString())
//         node.nodetype (Custom "valve")
//         node.data valve
//         node.position (n.XCoord, n.YCoord)
//         node.style [
//             style.width 50
//         ]
//     ]

// let splitNode (n: Shared.Layout.Node) (valve: Split) =
//     ReactFlow.node [
//         node.id (n.Id.ToString())
//         node.nodetype (Custom "split")
//         node.data valve
//         node.position (n.XCoord, n.YCoord)
//         node.style [
//             style.width 50
//         ]
//     ]


// let flowNodeToReactNode (frame: Playback.Frame) (n: Layout.Node) =
//     match n.Type with
//     | Layout.NodeType.Tank tankId -> tankNode n frame.Tanks.[tankId]
//     | Layout.NodeType.Valve valveId -> valveNode n frame.Valves.[valveId]
//     | Layout.NodeType.Split splitId -> splitNode n frame.Splits.[splitId]


// let flowArcToReactNode (frame: Playback.Frame) (a: Layout.Link) =
//     let data = frame.Links.[a.Id]
//     let label = $"%.2f{data.FlowRate}"

//     ReactFlow.edge [
//         edge.id (a.Id.ToString())
//         edge.source (a.SourceId.ToString())
//         edge.target (a.TargetId.ToString())
//         edge.animated (data.FlowRate > 0.0)
//         edge.label label
//         edge.edgeType EdgeType.Bezier
//         edge.arrowHeadType ArrowClosed
//         edge.style [
//             if data.FlowRate > 0.0 then style.stroke "blue"
//             else style.stroke "red"
//         ]
//         edge.labelStyle [
//             labelStyle.fill "black"
//             labelStyle.fontWeight 700
//         ]
//     ]


// let createFlowChartArea (model: Model) (dispatch: Msg -> unit) =
//     let frame = model.History.Frames.[model.FrameIdx]
//     let flowNodes =
//         model.Nodes
//         |> Seq.map (fun (KeyValue (_, n)) -> flowNodeToReactNode frame n)

//     let flowArcs =
//         model.Links
//         |> Seq.map (fun (KeyValue (_, a)) -> flowArcToReactNode frame a)

//     let reactNodes =
//         Seq.append flowNodes flowArcs
//         |> Array.ofSeq

//     Bulma.column [
// //        column.is9
//         prop.style [
//             style.flexGrow 1
//         ]
//         prop.children [

//             ReactFlow.flowChart [
//                 ReactFlow.nodesDraggable false
//                 ReactFlow.nodeTypes {| valve = Valve; tank = Tank; split = Split |}
//                 ReactFlow.elements reactNodes
//                 ReactFlow.children [
//                     ReactFlow.background [
//                         background.gap 20
//                         background.size 1.
//                         background.variant Dots
//                         background.color "lightgrey"
//                     ]
//                 ]
//                 ReactFlow.onElementClick
//                     (fun ev element ->
//                         let nodeIdString = string element.id
//                         match nodeIdString with
//                         | IsNodeId nodeId -> dispatch (NodeSelected nodeId)
//                         | _ -> ()
//                     )
//                 ReactFlow.onPaneClick
//                     (fun _ -> dispatch NodeDetailClosed)
//             ]
//         ]
//     ]


// let createNavbar (model: Model) (dispatch: Msg -> unit) =
//     Bulma.navbar [
//         prop.style [
//             Feliz.style.height (length.vh 5) // CSS :/
//         ]
//         Bulma.color.isPrimary
//     ]

// [<Emit"isNaN($0)">]
// let isNaN x = jsNative

// let numberInput componentId (inputValue: float) onChangeHandler =
//     Html.input [
//         prop.className "input"
//         prop.type' "number"
//         prop.step 0.01
//         prop.value inputValue
//         prop.onChange (fun (x: float) ->
//             if isNaN x then ()
//             else onChangeHandler (componentId, x)
//         )
//     ]

// let createTankDetail (time: TimeSpan) mode (tank: Tank) (history: array<TimeSpan * float>) (dispatch: Msg -> unit) =
//     let (TankId name) = tank.Id
//     let nowDateTime = DateTime.Now
//     let times = history |> Array.map fst |> Array.map (fun x -> nowDateTime + x)
//     let levels = history |> Array.map snd
//     let minLevel = Array.min levels
//     let maxLevel = Array.max levels
//     let timeDateTime = nowDateTime + time

//     Bulma.columns [
//         Bulma.column [
//             column.is2
//             prop.children [
//                 Html.div [
//                     Bulma.field.div [
//                         Bulma.label "Tank"
//                         Bulma.control.div [
//                             Bulma.text.div name
//                         ]
//                     ]
//                     Bulma.field.div [
//                         Bulma.label "Capacity"
//                         Bulma.control.div [
//                             match mode with
//                             | Playing ->
//                                 Bulma.text.div $"%.2f{tank.Capacity}"
//                             | Pausing ->
//                                 numberInput
//                                     tank.Id
//                                     tank.Capacity
//                                     (TankCapacityChanged >> dispatch)
//                         ]
//                     ]
//                     Bulma.field.div [
//                         Bulma.label "Level"
//                         Bulma.control.div [
//                             Bulma.text.div $"%.2f{tank.Level}"
//                         ]
//                     ]
//                     Bulma.field.div [
//                         Bulma.button.button [
//                             Bulma.color.isPrimary
//                             prop.disabled (not mode.isPausing)
//                             prop.text "Update"
//                             prop.onClick (fun _ -> if (not mode.isPausing) then () else dispatch (Msg.TankCapacityUpdated (tank.Id, tank.Capacity)))
//                         ]
//                     ]
//                 ]
//             ]
//         ]
//         Bulma.column [
//             Plotly.plot [
//                 plot.traces [
//                     traces.scatter [
//                         scatter.x times
//                         scatter.y levels
//                         scatter.mode [
//                             scatter.mode.lines
//                         ]
//                         scatter.line [
//                             line.shape.hv
//                         ]
//                     ]
//                     traces.scatter [
//                         scatter.x [timeDateTime; timeDateTime]
//                         scatter.y [minLevel; maxLevel]
//                         scatter.mode [
//                             scatter.mode.lines
//                         ]
//                         scatter.line [
//                             line.color color.gray
//                         ]
//                     ]
//                 ]
//                 plot.layout [
//                     layout.title "Tank Level"
//                     layout.showlegend false
//                 ]
//             ]
//         ]
//     ]

// let createSplitDetail mode (split: Split) (dispatch: Msg -> unit) =
//     let (SplitId name) = split.Id
//     Html.div [
//         Bulma.field.div [
//             Bulma.label "Split"
//             Bulma.control.div [
//                 Bulma.text.div name
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "Setting"
//             Bulma.control.div [
//                 match split.Setting with
//                 | SplitType.Off ->
//                     Html.text "Off"
//                 | SplitType.Single s ->
//                     Html.text "Single"
//                     Html.text (s.ToString())
//                 | SplitType.Priority priorityList ->
//                     Html.text "Priority"
//                     Html.ul [
//                         for p in priorityList do
//                             Html.li (p.ToString())
//                     ]
//             ]
//         ]
//     ]

// let createMergeDetail mode (merge: Merge) (dispatch: Msg -> unit) =
//     let (MergeId name) = merge.Id
//     Html.div [
//         Bulma.field.div [
//             Bulma.label "Merge"
//             Bulma.control.div [
//                 Bulma.text.div name
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "Setting"
//             Bulma.control.div [
//                 match merge.Setting with
//                 | MergeSetting.Off -> Html.text "Off"
//                 | MergeSetting.Single s -> Html.text (s.ToString())
//                 | MergeSetting.Priority priorityList ->
//                     Html.ul [
//                         for p in priorityList do
//                             Html.li (p.ToString())
//                     ]
//             ]
//         ]
//     ]

// let createConversionDetail mode (conversion: Conversion) (dispatch: Msg -> unit) =
//     let (ConversionId name) = conversion.Id
//     Html.div [
//         Bulma.field.div [
//             Bulma.label "Conversion"
//             Bulma.control.div [
//                 Bulma.text.div name
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "Coefficient"
//             Bulma.control.div [
//                 Bulma.text.div $"%.2f{conversion.Coefficient}"
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "In Rate"
//             Bulma.control.div [
//                 Bulma.text.div $"%.2f{conversion.InRate}"
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "Out Rate"
//             Bulma.control.div [
//                 Bulma.text.div $"%.2f{conversion.OutRate}"
//             ]
//         ]
//     ]

// let createConveyorDetail mode (conveyor: Conveyor) (dispatch: Msg -> unit) =
//     let (ConveyorId name) = conveyor.Id
//     Html.div [
//         Bulma.field.div [
//             Bulma.label "Conveyor"
//             Bulma.control.div [
//                 Bulma.text.div name
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "Max Velocity"
//             Bulma.control.div [
//                 Bulma.text.div $"%.2f{conveyor.MaxVelocity}"
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "Current Velocity"
//             Bulma.control.div [
//                 Bulma.text.div $"%.2f{conveyor.Velocity}"
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "In Rate"
//             Bulma.control.div [
//                 Bulma.text.div $"%.2f{conveyor.InRate}"
//             ]
//         ]
//         Bulma.field.div [
//             Bulma.label "Out Rate"
//             Bulma.control.div [
//                 Bulma.text.div $"%.2f{conveyor.OutRate}"
//             ]
//         ]
//     ]

// let createValveDetail (time: TimeSpan) mode (valve: Valve) (history: array<TimeSpan * float>) (dispatch: Msg -> unit) =
//     let (ValveId name) = valve.Id
//     let nowDateTime = DateTime.Now
//     let timeDateTime = nowDateTime + time
//     let times = history |> Array.map fst |> Array.map (fun x -> nowDateTime + x)
//     let flowRates = history |> Array.map snd
//     let minFlowRate = Array.min flowRates
//     let maxFlowRate = Array.max flowRates
//     Bulma.columns [
//         Bulma.column [
//             column.is2
//             prop.children [
//                 Html.div [
//                     Bulma.field.div [
//                         Bulma.label "Valve"
//                         Bulma.control.div [
//                             Bulma.text.div name
//                         ]
//                     ]
//                     Bulma.field.div [
//                         Bulma.label "Max Flow Rate"
//                         Bulma.control.div [
//                             match mode with
//                             | Playing ->
//                                 Bulma.text.div $"%.2f{valve.MaxFlowRate}"
//                             | Pausing ->
//                                 numberInput valve.Id valve.MaxFlowRate (ValveMaxFlowChanged >> dispatch)
//                         ]
//                     ]
//                     Bulma.field.div [
//                         Bulma.label "Flow Rate"
//                         Bulma.control.div [
//                             Bulma.text.div $"%.2f{valve.FlowRate}"
//                         ]
//                     ]
//                     match valve.Status with
//                     | ValveStatus.Satisfied -> Bulma.field.div []
//                     | ValveStatus.BlockedBy blockers ->
//                         Bulma.label "Blocked By"
//                         Html.ul [
//                             for b in blockers do
//                                 Html.li b
//                         ]
//                     | ValveStatus.StarvedBy starvers ->
//                         Bulma.label "Starved By"
//                         Html.ul [
//                             for s in starvers do
//                                 Html.li s
//                         ]
//                     | ValveStatus.BlockedAndStarvedBy (blockers, starvers) ->
//                         Bulma.label "Blocked By"
//                         Html.ul [
//                             for b in blockers do
//                                 Html.li b
//                         ]
//                         Bulma.label "Starved By"
//                         Html.ul [
//                             for s in starvers do
//                                 Html.li s
//                         ]
//                     | ValveStatus.FullyBlockedBy blockers ->
//                         Bulma.label "Fully Blocked By"
//                         Html.ul [
//                             for b in blockers do
//                                 Html.li b
//                         ]
//                     | ValveStatus.FullyStarvedBy starvers ->
//                         Bulma.label "Fully Starved By"
//                         Html.ul [
//                             for s in starvers do
//                                 Html.li s
//                         ]
//                     | ValveStatus.FullyBlockedAndStarvedBy (blockers, starvers) ->
//                         Bulma.label "Fully Blocked By"
//                         Html.ul [
//                             for b in blockers do
//                                 Html.li b
//                         ]
//                         Bulma.label "Fully Starved By"
//                         Html.ul [
//                             for s in starvers do
//                                 Html.li s
//                         ]

//                     Bulma.field.div [
//                         Bulma.button.button [
//                             Bulma.color.isPrimary
//                             prop.disabled (not mode.isPausing)
//                             prop.text "Update"
//                             prop.onClick (fun _ -> if (not mode.isPausing) then () else dispatch (Msg.ValveMaxFlowUpdated (valve.Id, valve.MaxFlowRate)))
//                         ]
//                     ]
//                 ]
//             ]
//         ]
//         Bulma.column [
//             Plotly.plot [
//                 plot.traces [
//                     traces.scatter [
//                         scatter.x times
//                         scatter.y flowRates
//                         scatter.mode [
//                             scatter.mode.lines
//                         ]
//                         scatter.line [
//                             line.shape.hv
//                         ]
//                     ]
//                     traces.scatter [
//                         scatter.x [timeDateTime; timeDateTime]
//                         scatter.y [minFlowRate * 0.9; maxFlowRate * 1.1]
//                         scatter.mode [
//                             scatter.mode.lines
//                         ]
//                         scatter.line [
//                             line.color color.gray
//                         ]
//                     ]
//                 ]
//                 plot.layout [
//                     layout.title "Flow Rate"
//                     layout.showlegend false
//                     layout.yaxis [
//                         yaxis.range [minFlowRate * 0.9; maxFlowRate * 1.1]
//                         yaxis.autorange.false'
//                     ]
//                 ]
//             ]
//         ]
//     ]


// let createDetailArea (model: Model) (n: Shared.Layout.Node) (dispatch: Msg -> unit) =
//     let frame = model.History.Frames.[model.FrameIdx]
//     Bulma.box [
//         prop.style [
//             Feliz.style.width (length.percent 100)
//         ]
//         prop.children [
//             Bulma.container [
//                 prop.children [
// //                    Bulma.icon [
// //                        Bulma.icon.isLarge
// //                        prop.onClick (fun _ -> Msg.NodeDetailClosed |> dispatch)
// //                        prop.children [
// //                            Html.i [ prop.className "fas fa-times-circle" ]
// //                        ]
// //                    ]
//                     match n.Type with
//                     | Shared.Layout.NodeType.Tank tankId -> createTankDetail frame.Time model.Mode frame.Tanks.[tankId] model.History.Tanks.[tankId] dispatch
//                     | Shared.Layout.NodeType.Valve valveId -> createValveDetail frame.Time model.Mode frame.Valves.[valveId] model.History.Valves.[valveId] dispatch
//                     | Shared.Layout.NodeType.Split splitId -> createSplitDetail model.Mode frame.Splits.[splitId] dispatch
//                     | Shared.Layout.NodeType.Merge mergeId -> createMergeDetail model.Mode frame.Merges.[mergeId] dispatch
//                     | Shared.Layout.NodeType.Conversion conversionId -> createConversionDetail model.Mode frame.Conversions.[conversionId] dispatch
//                     | Shared.Layout.NodeType.Conveyor conveyorId -> createConveyorDetail model.Mode frame.Conveyors.[conveyorId] dispatch
//                 ]

//             ]
//         ]
//     ]


// let createColumns (model: Model) (dispatch: Msg -> unit) =

//     match model.SelectedNode with
//     | Some n ->
//         [
//             createFlowChartArea model dispatch
//             Bulma.level [createDetailArea model n dispatch]
//         ]

//     | None ->
//         [
//             createFlowChartArea model dispatch
//         ]


// let controls model dispatch =
//     Bulma.box [
//         prop.style [
//             Feliz.style.height (length.px 180)
//             //Feliz.style.minHeight (length.px 170)
//         ]
//         prop.children [
//             Bulma.columns [
//                 columns.isCentered
//                 columns.isVCentered
//                 prop.children [
//                     Bulma.column [
//                         column.isHalf
//                         prop.children [
//                             Bulma.level [
//                                 Bulma.levelItem [
//                                     text.hasTextCentered
//                                     prop.children [
//                                         Bulma.button.button [
//                                             Bulma.color.isWhite
//                                             prop.onClick (fun _ -> dispatch MovedToStart)
//                                             prop.children [
//                                                 Bulma.icon [
//                                                     icon.isLarge
//                                                     prop.children [
//                                                         Html.i [
//                                                             prop.style [ style.transform.scaleX(-1) ]
//                                                             prop.className "fas fa-step-forward"
//                                                         ]
//                                                     ]
//                                                 ]
//                                             ]
//                                         ]
//                                     ]
//                                 ]
//                                 Bulma.levelItem [
//                                     text.hasTextCentered
//                                     prop.children [
//                                         Bulma.button.button [
//                                             Bulma.color.isWhite
//                                             prop.onClick (fun _ -> dispatch Rewind)
//                                             prop.children [
//                                                 Bulma.icon [
//                                                     icon.isLarge
//                                                     prop.children [
//                                                         Html.i [
//                                                             prop.style [ style.transform.scaleX(-1) ]
//                                                             prop.className "fas fa-redo-alt"
//                                                         ]
//                                                     ]
//                                                 ]
//                                             ]
//                                         ]
//                                     ]
//                                 ]
//                                 Bulma.levelItem [
//                                     text.hasTextCentered
//                                     prop.children [
//                                         Bulma.button.button [
//                                             Bulma.color.isWhite
//                                             prop.children [
//                                                 Bulma.icon [
//                                                     prop.onClick (fun _ ->
//                                                         if model.Mode.isPlaying then
//                                                             dispatch Paused
//                                                         else
//                                                             dispatch Resumed)
//                                                     icon.isLarge
//                                                     prop.children [
//                                                         Html.i [
//                                                             prop.style [
//                                                                 style.transform.scale 3
//                                                             ]
//                                                             prop.className
//                                                                 $"""fas fa-{if model.Mode.isPlaying then "pause-circle" else "play-circle"}"""
//                                                         ]
//                                                     ]
//                                                 ]
//                                             ]
//                                         ]
//                                     ]
//                                 ]
//                                 Bulma.levelItem [
//                                     text.hasTextCentered
//                                     prop.children [
//                                         Bulma.button.button [
//                                             Bulma.color.isWhite
//                                             prop.children [
//                                                 Bulma.icon [
//                                                     icon.isLarge
//                                                     prop.onClick (fun _ -> dispatch FastForward)
//                                                     prop.children [
//                                                         Html.i [ prop.className "fas fa-redo-alt" ]
//                                                     ]
//                                                 ]
//                                             ]
//                                         ]
//                                     ]
//                                 ]
//                                 Bulma.levelItem [
//                                     text.hasTextCentered
//                                     prop.children [
//                                         Bulma.button.button [
//                                             Bulma.color.isWhite
//                                             prop.onClick (fun _ -> dispatch MovedToEnd)
//                                             prop.children [
//                                                 Bulma.icon [
//                                                     icon.isLarge
//                                                     prop.children [
//                                                         Html.i [
//                                                             prop.style [ style.transform.scaleX(-1) ]
//                                                             prop.className "fas fa-step-backward"
//                                                         ]
//                                                     ]
//                                                 ]
//                                             ]
//                                         ]
//                                     ]
//                                 ]
//                                 Bulma.levelItem [
//                                     text.hasTextCentered
//                                     prop.children [
//                                         Bulma.button.button [
//                                             Bulma.color.isWhite
//                                             prop.children [
//                                                 Bulma.icon [
//                                                     prop.onClick (fun _ -> dispatch Reset)
//                                                     icon.isLarge
//                                                     prop.children [
//                                                         Html.i [ prop.className "fas fa-sync" ]
//                                                     ]
//                                                 ]
//                                             ]
//                                         ]
//                                     ]
//                                 ]
//                             ]
//                         ]
//                     ]
//                 ]
//             ]
//             Bulma.columns [
//                 columns.isCentered
//                 columns.isVCentered
//                 prop.children [
//                     Bulma.column [
//                         column.is2
//                         text.hasTextCentered
//                         prop.children [
//                             Html.div[
//                                 if model.History.Frames.Length > 0 then
//                                     prop.text (Helpers.TimeSpan.toString model.History.Frames.[model.FrameIdx].Time)
//                                 else
//                                     prop.text (Helpers.TimeSpan.toString TimeSpan.Zero)
//                             ]
//                         ]
//                     ]
//                     Bulma.column [
//                         column.is8
//                         text.hasTextCentered
//                         prop.children [
//                             if model.Mode.isPausing then
//                                 Browser.Dom.console.log $"FrameIdx: {model.FrameIdx}"
//                                 Slider.slider [
//                                     slider.isFullWidth
//                                     prop.step 1
//                                     prop.min 0
//                                     prop.max (model.History.Frames.Length - 1)
//                                     prop.value model.FrameIdx
//                                     prop.onChange (fun (v:string) ->
//                                         // let percent = (float v) / 100.0
//                                         // let newTime = model.EndTime * percent
//                                         let newIdx = int v
//                                         dispatch (IndexChanged newIdx)
//                                     )
//                                 ]
//                             else
//                                 Slider.slider [
//                                     slider.isFullWidth
//                                     prop.step 1
//                                     prop.min 0
//                                     prop.max (model.History.Frames.Length - 1)
//                                     prop.value model.FrameIdx
//                                 ]
//                         ]
//                     ]
//                     Bulma.column [
//                         column.is2
//                         text.hasTextCentered
//                         prop.children [
//                             Html.div [
//                                 prop.text (Helpers.TimeSpan.toString (model.EndTime))
//                             ]
//                         ]
//                     ]
//                 ]
//             ]
//         ]
//     ]

// let view (model: Model) (dispatch: Msg -> unit) =
//     Browser.Dom.console.log "view"
//     let diagramArea =
//         if model.FrameIdx >= 0 then
//             [createFlowChartArea model dispatch]
//         else
//             []

//     let detailArea =
//         match model.SelectedNode with
//         | Some n -> [createDetailArea model n dispatch]
//         | None -> []

//     Html.div [
//         prop.style [
//             style.minHeight (length.vh 100)
//             style.display.flex
//             style.flexDirection.column
//         ]
//         prop.children [

//             createNavbar model dispatch
//             if model.SelectedNode.IsSome then
//                 Bulma.level [
//                     prop.style [
//                         style.flexGrow 1
//                         style.display.flex
//                         style.flexDirection.column
//                     ]
//                     prop.children [
//                         Bulma.columns [
//                             prop.style [
//                                 style.flexGrow 1
//                                 style.display.flex
//                                 Feliz.style.width (length.percent 100)
//                             ]
//                             prop.children diagramArea
//                         ]
//                     ]
//                 ]
//                 Bulma.section [
//                     prop.children detailArea
//                 ]
//             else
//                 Bulma.level [
//                     prop.style [
//                         style.flexGrow 1
//                         style.display.flex
//                         style.flexDirection.column
//                     ]
//                     prop.children [
//                         Bulma.columns [
//                             prop.style [
//                                 style.flexGrow 1
//                                 style.display.flex
//                                 Feliz.style.width (length.percent 100)
//                             ]
//                             prop.children diagramArea
//                         ]
//                     ]
//                 ]
//             controls model dispatch
//         ]
//     ]



// module Layout

// open System.Collections.Generic
// open DiscreteRate
// open DiscreteRate.Types

// type Location = {
//     Column : int
//     Row : int
// }

// let private getSourcesAndTargets (links: list<Link>) =
//     let targets = Dictionary<NodeId, Set<NodeId>> ()
//     let mutable nodes = Set.empty
//     let mutable targetNodes = Set.empty

//     for link in links do
//         nodes <- nodes.Add link.Source.Id
//         nodes <- nodes.Add link.Target.Id
//         targetNodes <- targetNodes.Add link.Target.Id

//         match targets.TryGetValue link.Source.Id with
//         | true, t ->
//             targets.[link.Source.Id] <- t.Add link.Target.Id
//         | false, _ ->
//             targets.[link.Source.Id] <- Set.singleton link.Target.Id

//     let sources = nodes - targetNodes |> List.ofSeq
//     nodes, sources, (targets :> IReadOnlyDictionary<_,_>)

// let rec private createPlacements (targets: IReadOnlyDictionary<NodeId, Set<NodeId>>) (sources: list<NodeId>) =

//     let placements = Dictionary<NodeId, Location>()
//     let columnLastEntry = Dictionary<int, int>()

//     let rec loop (currentColumn: int) (sources: list<NodeId>) =
//         match sources with
//         | source::remainingSources ->
//             let nextRowIdx =
//                 match columnLastEntry.TryGetValue currentColumn with
//                 | true, lastRowIdx -> lastRowIdx + 1
//                 | false, _ -> 0
//             columnLastEntry.[currentColumn] <- nextRowIdx

//             let newLocation = { Column = currentColumn; Row = nextRowIdx}
//             placements.[source] <- newLocation
//             let sourceTargets =
//                 match targets.TryGetValue source with
//                 | true, x -> List.ofSeq x
//                 | false, _ -> []
//                 |> List.filter (fun t -> not (placements.ContainsKey t))

//             loop (currentColumn + 1) sourceTargets
//             loop currentColumn remainingSources

//         | [] -> ()

//     loop 0 sources
//     placements :> IReadOnlyDictionary<_,_>

// let private createLayoutNode (xOffset: int) (yOffset: int) (xScale: int) (yScale: int) (placements: IReadOnlyDictionary<NodeId, Location>) (nodeId: NodeId) =
//     let location = placements.[nodeId]
//     let nodeType =
//         // TODO: Fill in the rest of the cases
//         match nodeId with
//         | NodeId.TankId tankId -> Shared.Layout.NodeType.Tank tankId
//         | NodeId.ValveId valveId -> Shared.Layout.NodeType.Valve valveId
//         | NodeId.SplitId splitId -> Shared.Layout.NodeType.Split splitId
//         | NodeId.MergeId mergeId -> Shared.Layout.NodeType.Merge mergeId
//         | NodeId.ConversionId conversionId -> Shared.Layout.NodeType.Conversion conversionId
//         | NodeId.ConveyorId conveyorId -> Shared.Layout.NodeType.Conveyor conveyorId

//     {
//         Id = nodeId
//         XCoord = xOffset + xScale * location.Column
//         YCoord = yOffset + yScale * location.Row
//         Type = nodeType
//     } : Shared.Layout.Node

// let private generateLayoutNodes (links: list<Link>) =
//     let nodes, sources, targets = getSourcesAndTargets links
//     let placements = createPlacements targets sources

//     nodes
//     |> Seq.map (fun nodeId -> nodeId, createLayoutNode 50 50 200 150 placements nodeId)
//     |> Map

// let private generateLayoutLinks (links: list<Link>) =
//     links
//     |> List.map (fun link ->
//         let layoutLink : Shared.Layout.Link = {
//             Id = link.Id
//             SourceId = link.Source.Id
//             TargetId = link.Target.Id
//         }
//         link.Id, layoutLink )
//     |> Map


// let generateLayout (links: list<Link>) =
//     let layoutNodes = generateLayoutNodes links
//     let layoutLinks = generateLayoutLinks links
//     {
//         Nodes = layoutNodes
//         Links = layoutLinks
//     } : Shared.Layout.Layout

// //
// //module SplitStatus =
// //
// //    open Shared
// //
// //    let create (s: DiscreteRate.State.Split) =
// //
// //        match s.SplitType with
// //        | DiscreteRate.Types.SplitType.Off -> Playback.SplitStatus.Off
// //        | DiscreteRate.Types.SplitType.Single t -> Playback.SplitStatus.Single t
// //        | DiscreteRate.Types.SplitType.Priority t ->
// //            let ids = t |> List.map (fun x -> x.NodeId)
// //            Playback.SplitStatus.Priority ids


[|0 .. 0|]

let s1 = "Chicken"
let s2 = "Chicken"
s1 = s2

open System

let test () =
    let s = "Chicken"
    let s1 = s.AsSpan ()
    let ss = s1[^5]

    for e in ss do
        printfn $"{e}"

test ()


type IStaticDictionary<'Key, 'Value> =
    abstract member Item : 'Key -> 'Value with get

let valueDict =
    { new IStaticDictionary<'Key, 'Value> with
            member _.Item
                with get (k: 'Key) = Unchecked.defaultof<'Value> }

let strDict =
    { new IStaticDictionary<string, 'Value> with
        let values = [||]
        member _.Item
            with get (k: string) = Unchecked.defaultof<'Value> }

let refDict =
    { new IStaticDictionary<'Key, 'Value> with
        member _.Item
            with get (k: 'Key) = Unchecked.defaultof<'Value> }

let getStaticDict (entries: seq<'Key * 'Value>) : IStaticDictionary<'Key, 'Value> =
    if typeof<'Key>.IsValueType then
        valueDict
    elif typeof<'Key> = typeof<string> then
        strDict :?> IStaticDictionary<'Key, 'Value>
    else
        refDict

let x = t["Chicken"]
printfn $"{x}"

let x = 6uy
x + x

16 - 1

let m = [|
    1; 0; 0; 0
    0; 1; 0; 0
    1; 1; 1; 0
    0; 0; 0; 1
    0; 0; 1; 0
|]