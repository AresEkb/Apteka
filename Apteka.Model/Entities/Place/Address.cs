﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities.Place
{
    [DataElement("ru", "адрес", "адрес", "адреса", "")]
    public class Address
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "страна", "страна", "")]
        public Country Country { get; set; }

        [DataElement("ru", "город", "город", "")]
        public City City { get; set; }

        [DataElement("ru", "описание", "описание", "")]
        [MaxLength(200)]
        public string Description { get; set; }

        [DataElement("ru", "название", "название", "")]
        public string Name { get => String.Join(", ", new string[]
            { Country?.Name, City?.Name, Description }
            .Where(s => !String.IsNullOrWhiteSpace(s))); }
    }
}