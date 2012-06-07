namespace IronCondor

module Model =

    open System
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type dbSchema = SqlDataConnection<"Data Source=GBD03821801\SQLEXPRESS;Initial Catalog=FinancialData;Integrated Security=True"
                                        , Views = true, Functions = true>
    let db = dbSchema.GetDataContext()

    type Option     = {
        Symbol:                 string
        Type:                   string
        Expiry:                 string
        Days:                   int
        Strike:                 float
        Mid:                    float Nullable
        Last:                   float Nullable
        Bid:                    float Nullable
        Ask:                    float Nullable
        OpenInt:                float Nullable
        Volume:                 float Nullable
        ImplVol:                float Nullable
        Delta:                  float Nullable
        Theta:                  float Nullable
        Vega:                   float Nullable
        Gamma:                  float Nullable
        Rho:                    float Nullable       
        }

    let toOption (o: dbSchema.ServiceTypes.VCurrentOptions): Option =
        { Symbol = o.Symbol; Type = (if o.CallOrPut = 0 then "C" else "P"); Expiry = o.Expiry.ToString("MMM dd");
            Days = int (o.Expiry - o.TradeDate).TotalDays; Strike = o.Strike;
            Mid = Nullable ((o.Ask.Value + o.Bid.Value) / 2.); Last = o.Last; Bid = o.Bid; Ask = o.Ask; OpenInt = o.OpenInt; Volume = o.Volume;
            ImplVol = o.ImplVol; Delta = o.Delta; Theta = o.Theta; Vega = o.Vega; Gamma = o.Gamma; Rho = o.Rho }

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

    let loadLastDate () =
        let d = query {
                    for d in db.CurrentDate do
                    head
        }
        d.CurrentDate1
    
    let saveLastDate date =
        // one more db access than necessary
        let d = query {
                    for d in db.CurrentDate do
                    head
        }
        db.CurrentDate.DeleteOnSubmit(d)
        let newDate = dbSchema.ServiceTypes.CurrentDate (CurrentDate1 = date)
        db.CurrentDate.InsertOnSubmit(newDate) 
        db.DataContext.SubmitChanges()

    let stagedOptionToPorfolioPosition (o: StagingPosition): PortfolioPosition =
        {Name = o.Name; Strike = o.Strike; Price = o.Strike; Quantity = o.Quantity; Commission = 0.3}

    let portPosToStagedOption (o: PortfolioPosition): StagingPosition =
        {Name = o.Name; Strike = o.Strike; Price = o.Strike; Quantity = o.Quantity}

    let loadLastPositions ()  = Seq.empty :?> dbSchema.ServiceTypes.Options seq
//        [
//            {Name = "JQNN"; Strike = 1233.; Price = 3.4; Quantity = 200.}
//            {Name = "QERQ"; Strike = 123.; Price = 3.4; Quantity = 300.}
//            {Name = "JQNN"; Strike = 1233.; Price = 3.4; Quantity = 200.}
//            {Name = "QERQ"; Strike = 123.; Price = 3.4; Quantity = 300.}
//        ]

    let loadLastPortfolio (): PortfolioPosition list =
        [
            {Name = "JQNN"; Strike = 1233.; Price = 3.7; Quantity = 200.; Commission = 0.4}
            {Name = "QERQ"; Strike = 123.; Price = 3.4; Quantity = 300.; Commission = 0.4}
            {Name = "KFRS"; Strike = 234.; Price = 4.4; Quantity = 200.; Commission = 0.4}
            {Name = "CDFF"; Strike = 344.; Price = 3.2; Quantity = 300.; Commission = 0.4}
        ]

    let loadOptions callOrPut =
        let q = query {
            for p in db.VCurrentOptions do
            where (p.CallOrPut = callOrPut)
            where (System.Data.Linq.SqlClient.SqlMethods.Like(p.Symbol, "SPX%"))
            sortBy p.Expiry
            thenBy p.Strike 
            select p
        }
        q |> Seq.map toOption

    let loadCalls ()         = loadOptions 0
    let loadPuts ()          = loadOptions 1

    let persistStagedOptions (options: StagingPosition seq) =
        System.Diagnostics.Debug.WriteLine("Saved staged options: " + (Seq.length options).ToString())

    let persistPortfolioPositions (options: PortfolioPosition seq) =
        System.Diagnostics.Debug.WriteLine("Saved portfolio positions: " + (Seq.length options).ToString())

          
        