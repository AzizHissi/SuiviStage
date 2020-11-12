using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrstionDesStagiaires_V1
{
   public class CStage
    {
        public int id_stage;
        public string nom_stage;
        public DateTime date_debut = new DateTime();
        public DateTime date_fin = new DateTime();
        public float frais;
        List<CStage> listSatge = new List<CStage>();
        public CStage(  int id_stage,string nom_stage,DateTime date_debut , DateTime date_fin,float frais)
        {
            this.id_stage= id_stage;
            this.nom_stage= nom_stage;
            this.date_debut= date_debut;
            this.date_fin= date_fin;
            this.frais = frais;

        }
      
    }
}
