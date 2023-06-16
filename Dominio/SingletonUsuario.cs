using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class SingletonUsuario
    {
        private static SingletonUsuario instance;
        private static readonly object lockObject = new object();

        public SingletonUsuario()
        {
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
        }

        public SingletonUsuario(AccesoADatos.AcademicosUsuarios academicoUsuario)
        {
            this.IdUsuario = academicoUsuario.IdUsuario;
            this.NombreUsuario = academicoUsuario.NombreUsuario;
            this.Contrasena = academicoUsuario.Contrasena;
            this.AcademicosRoles = new ObservableCollection<AcademicosRoles>();
        }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }

        public ObservableCollection<AcademicosRoles> AcademicosRoles { get; set; }

        public int IdProgramaEducativo => this.AcademicosRoles.First().ProgramasEducativos.IdProgramaEducativo;

        public static SingletonUsuario Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonUsuario();
                        }
                    }
                }
                return instance;
            }
        }
        public void BorrarSinglenton()
        {
            SingletonUsuario.Instance.AcademicosRoles.Clear();
            SingletonUsuario.Instance.IdUsuario = -1;
            SingletonUsuario.Instance.Contrasena = "";
            SingletonUsuario.Instance.IdUsuario = -1;
            SingletonUsuario.Instance.NombreUsuario = "";
        }
    }
}
