open System
open System.Net

[<EntryPoint>]
let main argv =

    let method = Environment.GetEnvironmentVariable("LHP_METHOD")
    let port = Environment.GetEnvironmentVariable("LHP_PORT")
    let path = Environment.GetEnvironmentVariable("LHP_PATH")

    let req = HttpWebRequest.Create(Uri(sprintf "http://localhost:%s/%s" port path))
    req.Method <- method
    use resp = req.GetResponse()
    0
