namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type FigletSettings()  =
    inherit CommandSettings()

type FigletExample() =
    inherit Command<FigletSettings>()
    interface ICommandLimiter<FigletSettings>

    override _.Execute(_context, _settings) =
        "Star ..."
        |> customFiglet Left Color.SeaGreen1
        |> toConsole

        "Wars!"
        |> figlet
        |> toConsole
        0

type FigletDocumentation() =
    inherit Command<FigletSettings>()
    interface ICommandLimiter<FigletSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle

        EL |> toConsole
        pumped "Figlet module"
        |> alignedRule Left
        |> toConsole

        Many [
            Many [
                C "This module provides functionality from the figlet widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/figlet"
                C ")"
            ]
            EL
            C "The figlet can be used by the figlet function:"
            BI [
                P "figlet: string -> FigletText"
            ]
            EL
            C "This figlet will use the"
            BI [
                Many [P "Figlet.defaultAlignment,"; C "initialized to"; P "Center,"; C "and"]
                Many [P "Figlet.defaultColor,"; C "initialized to the pumped color"; P "Output.pumpedColor"; C ","]
            ]
            C "which both can be modified."
            EL
            C "Other rules can be used without changing the default by passing in the alignment as an argument to: "
            BI [
                P "customFiglet: Alignment -> Color -> string -> Renderable"
            ]
            EL
            Many [C "The figlet can be printed to the console with the"; P "toConsole"; C "function."]
        ] |> toConsole
        0