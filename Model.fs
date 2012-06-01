namespace IronCondor

module Model =

    open System

    type OptionType =
    | Call          = 0
    | Put           = 1

    type Option     = {
        Name:               string
        Strike:             float
        Price:              float
        }

    type StagingPosition   = {
        Name:               string
        Strike:             float
        Price:              float
        mutable Quantity:   float
    } 

    type PortfolioPosition = {
        Name:               string
        Strike:             float
        Price:              float
        mutable Quantity:   float
        Commission:         float
    }

    let loadLastDate () = DateTime.Now

    let optionToStagingPosition (o: Option): StagingPosition =
        {Name = o.Name; Strike = o.Strike; Price = o.Strike; Quantity = 200.}

    let stagedOptionToPorfolioPosition (o: StagingPosition): PortfolioPosition =
        {Name = o.Name; Strike = o.Strike; Price = o.Strike; Quantity = o.Quantity; Commission = 0.3}

    let portPosToStagedOption (o: PortfolioPosition): StagingPosition =
        {Name = o.Name; Strike = o.Strike; Price = o.Strike; Quantity = o.Quantity}

    let loadLastPositions (): StagingPosition list =
        [
            {Name = "JQNN"; Strike = 1233.; Price = 3.4; Quantity = 200.}
            {Name = "QERQ"; Strike = 123.; Price = 3.4; Quantity = 300.}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4; Quantity = 200.}
            {Name = "QERQ"; Strike = 123.; Price = 3.4; Quantity = 300.}
        ]

    let loadLastPortfolio (): PortfolioPosition list =
        [
            {Name = "JQNN"; Strike = 1233.; Price = 3.7; Quantity = 200.; Commission = 0.4}
            {Name = "QERQ"; Strike = 123.; Price = 3.4; Quantity = 300.; Commission = 0.4}
            {Name = "KFRS"; Strike = 234.; Price = 4.4; Quantity = 200.; Commission = 0.4}
            {Name = "CDFF"; Strike = 344.; Price = 3.2; Quantity = 300.; Commission = 0.4}
        ]


    let loadCalls date: Option list =
        [
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
        ]    

    let loadPuts date: Option list =
        [
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
            {Name = "JQNN"; Strike = 1233.; Price = 3.4}
            {Name = "QERF"; Strike = 1518.; Price = 2.4}
            {Name = "KFJB"; Strike = 2400.; Price = 4.3}
            {Name = "PRST"; Strike = 3200.; Price = 6.8}
        ]

    let persistStagedOptions (options: StagingPosition seq) =
        System.Diagnostics.Debug.WriteLine("Saved staged options: " + (Seq.length options).ToString())

    let persistPortfolioPositions (options: PortfolioPosition seq) =
        System.Diagnostics.Debug.WriteLine("Saved portfolio positions: " + (Seq.length options).ToString())

          
        