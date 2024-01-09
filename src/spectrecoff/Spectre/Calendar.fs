[<AutoOpen>]
module SpectreCoff.Calendar

open Spectre.Console
open SpectreCoff.Output

type Culture = Culture of string
let unwrapCulture (Culture c) = c

type Year = Year of int
[<RequireQualifiedAccess>]
module Year =
    let unwrap (Year y) = y

type Month = Month of int
[<RequireQualifiedAccess>]
module Month =
    let unwrap (Month m) = m

type Day = Day of int
[<RequireQualifiedAccess>]
module Day =
    let unwrap (Day d) = d

type Event = Event of Year * Month * Day
[<RequireQualifiedAccess>]
module Event =
    let unwrap (Event (Year y, Month m, Day d)) = (y, m, d)

type CalendarSettings =
    { Culture: Culture Option;
      HideHeaders: bool;
      HeaderLook: Look;
      HighlightLook: Look; }

let mutable defaultCalendarSettings =
    { Culture =  None
      HideHeaders = false
      HeaderLook = { calmLook with Decorations = [ Decoration.Bold ] }
      HighlightLook = { pumpedLook with Decorations = [ Decoration.Invert ] } }


let private applysettings (settings: CalendarSettings) (calendar: Calendar) =
    match settings.Culture with
    | Some culture -> calendar.Culture <- unwrapCulture culture |> System.Globalization.CultureInfo
    | _ -> ()

    calendar.ShowHeader <- not settings.HideHeaders
    calendar.HeaderStyle <- toSpectreStyle settings.HeaderLook
    calendar.HighlightStyle <- toSpectreStyle settings.HighlightLook
    calendar

let addEvent event (calendar: Calendar) =
    event
    |> Event.unwrap
    |> calendar.AddCalendarEvent

let customCalendar settings year month =
    Calendar (Year.unwrap year, Month.unwrap month)
    |> applysettings settings

let calendar =
    customCalendar defaultCalendarSettings

type Calendar with
    member self.toOutputPayload = toOutputPayload self
