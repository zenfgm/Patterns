using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public class Polzovatel
    {
        public string Imya { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

        public Polzovatel(string imya, string email, string rol)
        {
            Imya = imya;
            Email = email;
            Rol = rol;
        }
    }

    public class MenedzherPolzovateley
    {
        private List<Polzovatel> polzovateli = new List<Polzovatel>();

        public void DobavitPolzovatelya(string imya, string email, string rol)
        {
            var polzovatel = new Polzovatel(imya, email, rol);
            polzovateli.Add(polzovatel);
        }

        public void UdalitPolzovatelya(string email)
        {
            var polzovatel = polzovateli.FirstOrDefault(p => p.Email == email);
            if (polzovatel != null)
            {
                polzovateli.Remove(polzovatel);
            }
        }

        public void ObnovitPolzovatelya(string email, string novoeImya, string novayaRol)
        {
            var polzovatel = polzovateli.FirstOrDefault(p => p.Email == email);
            if (polzovatel != null)
            {
                polzovatel.Imya = novoeImya;
                polzovatel.Rol = novayaRol;
            }
        }

        public List<Polzovatel> PoluchitSpisokPolzovateley()
        {
            return polzovateli;
        }
    }
}
