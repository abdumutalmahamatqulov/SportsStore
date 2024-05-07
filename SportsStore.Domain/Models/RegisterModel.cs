using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Models;

public class RegisterModel : LoginModel
{
	public string Name { get; set; }
}
