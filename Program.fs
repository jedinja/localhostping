﻿open System
open System.IO
open System.Net
open System.Security.Cryptography.X509Certificates

[<EntryPoint>]
let main argv =

    let method = Environment.GetEnvironmentVariable "LHP_METHOD"
    let port = Environment.GetEnvironmentVariable "LHP_PORT"
    let path = Environment.GetEnvironmentVariable "LHP_PATH"
    let body = Environment.GetEnvironmentVariable "LHP_BODY"
    let certPath = Environment.GetEnvironmentVariable "LHP_CERT"
    let postBytes = body |> System.Text.Encoding.ASCII.GetBytes
    
    printf "ENV: %s %s %s %s %s" method port path body certPath
    
    let req = HttpWebRequest.Create(Uri(sprintf "https://localhost:%s/%s" port path)) :?> HttpWebRequest
    req.Method <- method
    req.ContentLength <- postBytes.LongLength
    req.ContentType <- "application/json"

    match certPath with
    | null | "" -> ()
    | fileName ->
        new X509Certificate2(File.ReadAllBytes fileName)
        |> req.ClientCertificates.Add
        |> ignore

    use reqStr = req.GetRequestStream()
    reqStr.Write(postBytes, 0, postBytes.Length)

    printf "After request stream"
    
    use resp = req.GetResponse()
    printf "After resp stream"
    
    0
