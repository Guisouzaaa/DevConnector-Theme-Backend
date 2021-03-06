﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectorBackEnd.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public string Bio { get; set; }
        public string ProfilePic { get; set; }
        public string Location { get; set; }
        public string ProfessionalStatus { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }
        public string Skills { get; set; }
        public string GithubUsername { get; set; }
        public string TwitterUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string InstagramUrl { get; set; }
    }
}
