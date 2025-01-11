/*using System;

class Program
{
    static void Main(string[] args)
    {
        Uri info = new Uri("http://www.domain.com:80/info?id=123#fragment");
        Uri page = new Uri("http://www.domain.com/info/page.html");

        Console.WriteLine($"Host: {info.Host}");
        Console.WriteLine($"Port: {info.Port}");
        Console.WriteLine($"PathAndQuery: {info.PathAndQuery}");
        Console.WriteLine($"Query: {info.Query}");
        Console.WriteLine($"Fragment: {info.Fragment}");
        Console.WriteLine($"Default HTTP port: {page.Port}");
        Console.WriteLine($"IsBaseOf: {info.IsBaseOf(page)}");

        Uri relative = info.MakeRelativeUri(page);
        Console.WriteLine($"IsAbsoluteUri: {relative.IsAbsoluteUri}");
        Console.WriteLine($"RelativeUri: {relative.ToString()}");

        Console.ReadLine();
    }
}*/



using System;
using System.IO;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        
        WebRequest request = WebRequest.Create("http://www.contoso.com/default.html");     
        request.Credentials = CredentialCache.DefaultCredentials;    
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();    
        Console.WriteLine("Status: " + response.StatusDescription);
        Console.WriteLine(new string('*', 50));      
        Stream dataStream = response.GetResponseStream();     
        StreamReader reader = new StreamReader(dataStream);       
        string responseFromServer = reader.ReadToEnd();     
        Console.WriteLine(responseFromServer);
        Console.WriteLine(new string('*', 50));      
        reader.Close();
        dataStream.Close();
        response.Close();
    }
}
