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
        let mutable calls           = loadCalls date
        let mutable selectedOptions: IList          = ArrayList() :> IList
        let mutable selectedStagedOptions: IList    = ArrayList() :> IList
        let mutable selectedPortOptions: IList      = ArrayList() :> IList

        let mutable puts    = loadCalls date

        let staging                 = new ObservableCollection<StagingPosition>(loadLastPositions ())
        let portfolio               = new ObservableCollection<PortfolioPosition>(loadLastPortfolio ())
        
        member x.Staging            = staging
        member x.Portfolio          = portfolio
  
        member x.SelectedOptions
            with get()              = selectedOptions
            and set(v : obj)        = 
                selectedOptions     <- v :?> Collections.IList 
        
        member x.SelectedStagedOptions
            with get()              = selectedStagedOptions
            and set(v : obj)        = 
                selectedStagedOptions     <- v :?> Collections.IList 
        
        member x.SelectedPortOptions
            with get()              = selectedPortOptions
            and set(v : obj)        = 
                selectedPortOptions <- v :?> Collections.IList 
      
        member x.Date
            with get()              = date
            and set(v : obj)        = 
                date                <- v :?> DateTime
                x.OnPropertyChanged(<@ x.Date @>)

        member x.Calls
            with get()              = calls
            and set(v : obj)        = 
                calls               <- v :?> Option list
                x.OnPropertyChanged(<@ x.Calls @>) 

        member x.Puts
            with get()              = puts
            and set(v : obj)        = 
                puts                <- v :?> Option list
                x.OnPropertyChanged(<@ x.Puts @>) 

        member x.NextDay ()         =
            x.Date                  <- x.Date.AddDays(+1.)
            x.Calls                 <- loadCalls x.Date
            x.Puts                  <- loadPuts x.Date
        
        member x.StageOptions ()    =
            let stagedOptions       =
                selectedOptions
                |> IListToSeq
                |> Seq.cast<Option>
                |> Seq.map optionToStagingPosition
            stagedOptions |> Seq.iter x.Staging.Add
            persistStagedOptions stagedOptions

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
            stagedPos |> Seq.iter x.Staging.Add
            persistStagedOptions stagedPos

