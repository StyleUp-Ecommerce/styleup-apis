﻿using CleanBase.Core.Entities.Base;
using CleanBase.Core.ViewModels.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Pet
{
	public class PetRequest : EntityRequestBase, IKeyObject
	{
		public int Age { get; set; }
	}
}
