//------------------------------------------------------------------------------  
// <auto-generated>                                                               
// This code was generated by QuickCode.                                          
// Runtime Version:1.0                                                            
//                                                                                
// Changes to this file may cause incorrect behavior and will be lost if          
// the code is regenerated.                                                       
// </auto-generated>                                                              
//------------------------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace QuickCode.Demo.EmailManagerModule.Application.Dtos
{
    public record EmailSendersDto
    {
        public int Id { get; init; }
        public string GsmNumber { get; init; }
        public string ProviderName { get; init; }
    }
}