//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_Ajedrez_v_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class comentario
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public int articulo_id { get; set; }
        public string comentario1 { get; set; }
        public long comen_estado_id { get; set; }
        public System.DateTime fechaCreacion { get; set; }
    
        public virtual articulo articulo { get; set; }
        public virtual comentario_estado comentario_estado { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
