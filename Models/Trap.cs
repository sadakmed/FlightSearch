using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq;

namespace travel.Models
    {
    
    public class Trap{
        public string origine {get ; set;}
        public string destination {get; set;}
        public List<quote> Q {get; set;} 
    }
    public class quote{
        public quote()
        
        {}
        
        public quote(string date,float price,string airline)
        {
            this.date=date;
            this.price=price;
            this.airline=airline;     
        }
        public string date   {get; set;}
        public float  price  {get; set;}
        public string airline{get; set;}
    }
    
    
    }
    