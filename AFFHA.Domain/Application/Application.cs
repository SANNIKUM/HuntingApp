using System;
using System.Collections.Generic;
using System.Text;

namespace AFFHA.Domain.Application
{
   public class Application : BaseEntityTrackable
    {
        /// <summary>
        /// Name of an application
        /// </summary>
        public string Name { get; set; }

        public string AppId { get; set; }
        public byte[] ApiKeySalt { get; set; }
        public byte[] ApiKeyHash { get; set; }
    }
}
