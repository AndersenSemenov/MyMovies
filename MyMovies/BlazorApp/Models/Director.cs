﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models
{
    public class Director: Person
    {
        public Director(string id, string name) : base(id, name) { }
    }
}
