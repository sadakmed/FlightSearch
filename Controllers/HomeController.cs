using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using travel.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace travel.Controllers
{
    public class HomeController : Controller
    {
        // just to see how the response is? so that you can have ana idea about what do u want to display
        public IActionResult Index()
        {
            // string url="https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/browsedates/v1.0/MA/MAD/en-US/MXP-sky/CMN-sky/2019-01-20?inboundpartialdate=2019-02-01";
            // var jsonData =  Get<trip>(url); 
              //         Console.WriteLine(jsonData);
            
            var jsond="{\"Dates\":{\"OutboundDates\":[{\"PartialDate\":\"2019-01-20\",\"QuoteIds\":[1,2],\"Price\":1134.0,\"QuoteDateTime\":\"2019-01-10T10:39:00\"}]},\"Quotes\":[{\"QuoteId\":1,\"MinPrice\":1134.0,\"Direct\":false,\"OutboundLeg\":{\"CarrierIds\":[1760],\"OriginId\":68768,\"DestinationId\":45198,\"DepartureDate\":\"2019-01-20T00:00:00\"},\"QuoteDateTime\":\"2019-01-10T10:39:00\"},{\"QuoteId\":2,\"MinPrice\":1457.0,\"Direct\":true,\"OutboundLeg\":{\"CarrierIds\":[852],\"OriginId\":68768,\"DestinationId\":45198,\"DepartureDate\":\"2019-01-20T00:00:00\"},\"QuoteDateTime\":\"2019-01-10T10:39:00\"}],\"Places\":[{\"PlaceId\":45198,\"IataCode\":\"CMN\",\"Name\":\"CasablancaMohamedV.\",\"Type\":\"Station\",\"SkyscannerCode\":\"CMN\",\"CityName\":\"Casablanca\",\"CityId\":\"CASA\",\"CountryName\":\"Morocco\"},{\"PlaceId\":68768,\"IataCode\":\"MXP\",\"Name\":\"MilanMalpensa\",\"Type\":\"Station\",\"SkyscannerCode\":\"MXP\",\"CityName\":\"Milan\",\"CityId\":\"MILA\",\"CountryName\":\"Italy\"}],\"Carriers\":[{\"CarrierId\":852,\"Name\":\"RoyalAirMaroc\"},{\"CarrierId\":1760,\"Name\":\"TAP\"}],\"Currencies\":[{\"Code\":\"MAD\",\"Symbol\":\"ﺩ.ﻡ.<200f>\",\"ThousandsSeparator\":\",\",\"DecimalSeparator\":\".\",\"SymbolOnLeft\":true,\"SpaceBetweenAmountAndSymbol\":true,\"RoundingCoefficient\":\"DecimalDigits\":2}]}";
             //Console.WriteLine(jsond);
             JObject resultJSon=JObject.Parse(jsond);
           
              Console.WriteLine(resultJSon["Dates"]["OutboundDates"][0]["PartialDate"].ToString());
                        return View();
        }

        public IActionResult flight()

        { 

                try
                {
                        string ori=Request.Form["origin"].ToString();
                        string des=Request.Form["dest"].ToString();
                        string  dat= Request.Form["date"].ToString();
                        string  datr= Request.Form["datR"].ToString();
                        Trap tr = new Trap();
                        Trap trr = new Trap();
                        List<Trap> listTr = new List<Trap>();
                        tr=GetTrap(ori,des,dat);
                  
                      
                         
                        if(tr.origine!= null )
                        {     
                            listTr.Add(tr); 

                        }
                        if(tr.origine== null && String.IsNullOrEmpty(datr))
                        {  
                            ViewData["row"]="No results found!!";
                            return View();
                        }else if (tr.origine!= null && !String.IsNullOrEmpty(datr))
                        {
                            trr=GetTrap(des,ori,datr);
                          
                        if(trr.origine!=null)
                        {  
                            listTr.Add(trr);
                        }
                       
                        return  View(listTr);
                        }
                        return View(listTr);
                }
                
                catch
                {
                        ViewData["row"]="No results found!!!";
                        return View();
                }
        }


        public async Task<string> getFromURLAsync(string url)
        {
                    
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", "e13de4a7c6mshbbe8479bdf87bb7p1b4f17jsn8b13456121f6");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                var json = await client.GetStringAsync(url);
                Console.WriteLine("send request");
                return json;
            }
        }

        public Trap GetTrap(string ori,string des,string dat)
        {
            string url= "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/browsedates/v1.0/MA/MAD/en-US/"+ori+"-sky/"+des+"-sky/"+dat ;
            
            
            

            
            JObject resultJSon=JObject.Parse(getFromURLAsync(url).Result); 
            Trap tr = new Trap() ;
            List<quote> qt = new List<quote> () ;
            string air="hi";
            if(resultJSon["Quotes"].Count()!=0)    
            {     
                for (var i =0 ; i < resultJSon["Quotes"].Count();i++)
                {

                    if( ori.Equals(resultJSon["Places"][i]["IataCode"].ToString())  )
                           {
                              tr.origine=resultJSon["Places"][i]["Name"]+" - "+resultJSon["Places"][i]["CountryName"];
                           }
                    if( des.Equals(resultJSon["Places"][i]["IataCode"].ToString())  )
                           {
                              tr.destination=resultJSon["Places"][i]["Name"]+" - "+resultJSon["Places"][i]["CountryName"];
                           }                                                                          
                                
                         
                    int idairline=(int)resultJSon["Quotes"][i]["OutboundLeg"]["CarrierIds"][0];
                
                    for (var j=0 ; j<resultJSon["Carriers"].Count();j++)
                            { 
                                    if(idairline == (int)resultJSon["Carriers"][j]["CarrierId"])
                                    {
                                        air= resultJSon["Carriers"][j]["Name"].ToString();   
                                    }
                            }
                    quote q = new quote(resultJSon["Quotes"][i]["OutboundLeg"]["DepartureDate"].ToString(),(float)resultJSon["Quotes"][i]["MinPrice"],air );
                    qt.Add(q);
                }

                
                tr.Q=qt;  
                return tr;
            }
            tr.origine=null;
            return tr ;
     }


    






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }}

