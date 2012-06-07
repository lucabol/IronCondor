#if INTERACTIVE
    #r "WindowsBase.dll"
    #r "PresentationCore.dll"
    #r "PresentationFramework.dll"
    #r "System.Xaml"
    #r @"C:\Projects\IronCondor\bin\Debug\FSharpx.Core.dll"
    #r @"C:\Projects\IronCondor\bin\Debug\FSharpx.TypeProviders.dll"
#else
namespace IronCondor
module window =
#endif

    open System
    open System.Windows
    open System.Windows.Controls

    open FSharpx
    open OxyPlot

    open IronCondor.ViewModel

    type MainWindow = XAML<"Window.xaml">

    let loadWindow() =
        let window = MainWindow()
        let vm = ViewModel()
        window.MainWindow.DataContext <- vm

        // Hooking up events
        window.NextDay.Click.Add (fun _ -> vm.NextDay 1.)
        window.PrevDay.Click.Add (fun _ -> vm.NextDay -1.)
        window.Calls.SelectionChanged.Add(fun _ -> vm.SelectedOptions <- window.Calls.SelectedItems) 
        window.Puts.SelectionChanged.Add(fun _ -> vm.SelectedOptions <- window.Puts.SelectedItems) 
        window.StageOptions.Click.Add(fun _ -> vm.StageOptions ())

        window.Staging.SelectionChanged.Add (fun _ -> vm.SelectedStagedOptions <- window.Staging.SelectedItems)
        window.BuyOptions.Click.Add( fun _ -> vm.BuyOptions ())
        window.Portfolio.SelectionChanged.Add (fun _ -> vm.SelectedPortOptions <- window.Portfolio.SelectedItems)
        window.Restage.Click.Add( fun _ -> vm.Restage ())

        window.MainWindow

    let private main (args: string []) =
        let app = new Application()
        app.ShutdownMode <- ShutdownMode.OnMainWindowClose
        app.Run(loadWindow())

    #if INTERACTIVE
    fsi.CommandLineArgs |> Array.toList |> List.tail |> List.toArray |> main
    #else
    [<EntryPoint; STAThread>]
    let entryPoint args = main args
    #endif

