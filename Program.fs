open System
open System.Net

[<EntryPoint>]
let main argv =

    let method = Environment.GetEnvironmentVariable("LHP_METHOD")
    let port = Environment.GetEnvironmentVariable("LHP_PORT")
    let path = Environment.GetEnvironmentVariable("LHP_PATH")
    let body = Environment.GetEnvironmentVariable("LHP_BODY")

    let postBytes = body |> System.Text.Encoding.ASCII.GetBytes

    let req = HttpWebRequest.Create(Uri(sprintf "http://localhost:%s/%s" port path))
    req.Method <- method
    req.ContentLength <- postBytes.LongLength
    req.ContentType <- "application/json"
    use reqStr = req.GetRequestStream()
    reqStr.Write(postBytes, 0, postBytes.Length)

    use resp = req.GetResponse()
    0
