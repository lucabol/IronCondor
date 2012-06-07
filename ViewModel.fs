namespace IronCondor

module ViewModel =

    open System
    open System.Collections.ObjectModel
    open System.Collections

    open OxyPlot
    open IronCondor.Model

    let IListToSeq (l: Collections.IList) = seq { for o in l -> o}

    type ViewModel() =
        inherit Utils.ViewModelBase()

        let mutable date            = loadLastDate ()
        let mutable selectedOptions: IList          = ArrayList() :> IList
        let mutable selectedStagedOptions: IList    = ArrayList() :> IList
        let mutable selectedPortOptions: IList      = ArrayList() :> IList

        let mutable puts            = loadPuts ()
        let mutable calls           = loadCalls ()

        let staging                 = new ObservableCollection<Option>()//(loadLastPositions ())
        let portfolio               = new ObservableCollection<PortfolioPosition>(loadLastPortfolio ())
        
        member x.Staging            = staging
        member x.Portfolio          = portfolio
  
        member x.SelectedOptions
            with get()              = selectedOptions
            and set(v)              = 
                selectedOptions     <- v 
        
        member x.SelectedStagedOptions
            with get()              = selectedStagedOptions
            and set(v)              = 
                selectedStagedOptions     <- v
        
        member x.SelectedPortOptions
            with get()              = selectedPortOptions
            and set(v)              = 
                selectedPortOptions <- v 
      
        member x.Date
            with get()              = date
            and set(v)              = 
                date                <- v
                saveLastDate        date
                x.Calls             <- loadCalls ()
                x.Puts              <- loadPuts  ()
                x.OnPropertyChanged(<@ x.Date @>)

        member x.Calls
            with get()              = calls
            and set(v)              = 
                calls               <- v
                x.OnPropertyChanged(<@ x.Calls @>) 

        member x.Puts
            with get()              = puts
            and set(v)              = 
                puts                <- v
                x.OnPropertyChanged(<@ x.Puts @>) 

        member x.NextDay i          =
            x.Date                  <- x.Date.AddDays(i)
            x.Calls                 <- loadCalls ()
            x.Puts                  <- loadPuts  ()
        
        member x.StageOptions ()    =
            for o in selectedOptions do
                x.Staging.Add (o :?> Option)
            //persistStagedOptions stagedOptions

        member x.BuyOptions ()      =
            let portPos             =
                selectedStagedOptions
                |> IListToSeq
                |> Seq.cast<StagingPosition>
                |> Seq.map stagedOptionToPorfolioPosition
            portPos |> Seq.iter x.Portfolio.Add
            persistPortfolioPositions portPos

        member x.Restage ()         =
            let stagedPos           =
                selectedPortOptions
                |> IListToSeq
                |> Seq.cast<PortfolioPosition>
                |> Seq.map portPosToStagedOption
            //stagedPos |> Seq.iter x.Staging.Add
            persistStagedOptions stagedPos

