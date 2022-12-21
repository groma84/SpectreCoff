[<AutoOpen>]
module SpectreCoff.Tree

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

type GuideStyle =
    | Ascii
    | SingleLine
    | DoubleLine
    | BoldLine

type TreeLayout =
    {  Sizing: SizingBehaviour
       Guides: GuideStyle }

let defaultTreeLayout: TreeLayout =
    {  Sizing = Collapse
       Guides = SingleLine }

let toOutputPayload tree =
    tree 
    :> Rendering.IRenderable 
    |> Renderable

let private applyLayout layout (root: Tree) =
    match layout.Sizing with 
    | Expand -> root.Expanded <- true
    | Collapse -> root.Expanded <- false

    match layout.Guides with
    | Ascii -> root.Guide <- TreeGuide.Ascii
    | SingleLine -> root.Guide <- TreeGuide.Line
    | DoubleLine -> root.Guide <- TreeGuide.DoubleLine
    | BoldLine -> root.Guide <- TreeGuide.BoldLine
    root

let attach (nodes: TreeNode list) (node: TreeNode) =
    nodes 
    |> List.iter (fun n -> node.AddNode n |> ignore)
    node

let createNode (content: OutputPayload) (nodes: TreeNode list) = 
    let node = TreeNode (content |> payloadToRenderable ) 
    attach nodes node

let customTree (layout: TreeLayout) (rootContent: OutputPayload ) (nodes: TreeNode list) =
    let tree = Tree(rootContent |> payloadToRenderable) |> applyLayout layout
    nodes |> List.iter (fun node -> tree.AddNode node |> ignore)
    tree

let tree: OutputPayload -> TreeNode list -> Tree =
    customTree defaultTreeLayout

type Tree with 
    member self.toOutputPayload = toOutputPayload self 
