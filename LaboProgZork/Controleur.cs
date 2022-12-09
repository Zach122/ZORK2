// Classe Program
//
// Contrôleur principal du jeu
// Utilise les instances des classes :
//  - InterfaceUtilisateur : affichage et interactions
//  - Modele : statistiques, habiletés
//  - Joueur : statistiques, habiletés, attaquer, défendre
//  - Ennemi : statistiques, habiletés, attaquer, défendre
//
// Le jeu se déroule selon l'état (variable etat)
// 1. Écran d'acceuil et menu initial (jouer - quitter)
// 2 et 3. Exploration du labyrinthe et combats
// 4. Fin et menu de fin (rejouer - quitter)
// 
// Pendant l'exploration et le combat on alterne entre les états 2 et 3
// 2. Affichage de la carte et déplacement du personnage
// 3. Combat entre personnage et ennemi(s)
//
// Création : 2022/11/19
// Par : Frédérik Taleb
// Modification : 2022/11/24
// Par : Frédérik Taleb

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboFinal_A22
{
    public class Controleur
    {
        InterfaceUtilisateur ui;
        Modele usine;
        Joueur joueur;
        string[] classes;
        Ennemi[] ennemis;
        int etat;
        int encounter;

        public Controleur(string[] tabNomEnnemis)
        {
            this.etat = 0;
            this.ui = new InterfaceUtilisateur();
            this.usine = new Modele();
            this.classes = new string[3] { "guerrier", "roublard", "magicien"};
            this.ennemis = new Ennemi[tabNomEnnemis.Length];
            for (int i = 0; i < tabNomEnnemis.Length; i++)
            {
                ennemis[i] = usine.genererEnnemi(tabNomEnnemis[i]);
            }
            this.encounter = 0;
        }

        public void jouer()
        {
            bool jouer = true;
            while (jouer)
            {
                switch (this.etat)
                {
                    case 0:
                        intro();
                        break;
                    case 1:
                        creation();
                        break;
                    case 2:
                        exploration();
                        break;
                    case 3:
                        combat();
                        break;
                    case 4:
                        finale();
                        break;
                    default:
                        jouer = false;
                        break;
                }
            }
        }

        public void intro()
        {
            Console.Clear();
            this.etat = -1;
            this.ui.afficherEntete();
            if (this.ui.afficherMenuIntro() == 1)
            {
                this.etat = 1;
            }
        }

        public void creation()
        {
            Console.Clear();
            this.ui.afficherEntete();
            string nom = this.ui.demanderNom();
            int classe = this.ui.afficherMenuCreation();
            this.joueur = this.usine.genererJoueur(this.classes[classe], nom);
            this.etat = 2;
        }

        public void exploration()
        {
            Random rng = new Random();
            Console.Clear();
            this.ui.afficherEntete();
            this.ui.afficherCarte();
            int dir = this.ui.afficherMenuExploration();
            if (this.ui.deplacerJoueur(dir))
            {
                this.etat = 4;
            }
            else 
            {
                this.encounter += 1;
            }
            if (rng.Next(50, 101) <= this.encounter)
            {
                this.etat = 3;
            }
        }

        public void combat()
        {
            Random rng = new Random();
            Ennemi[] combatants = new Ennemi[rng.Next(1,4)];
            for (int i = 0; i < combatants.Length; i++)
            {
                int combatant = rng.Next(this.ennemis.Length);
                combatants[i] = new Ennemi(
                    this.ennemis[combatant].nom,
                    this.ennemis[combatant].att,
                    this.ennemis[combatant].matt,
                    this.ennemis[combatant].def,
                    this.ennemis[combatant].mdef,
                    this.ennemis[combatant].hp,
                    this.ennemis[combatant].magique
                    );
            }

            bool combat = true;
            this.joueur.habilete.tour = 0;
            while (combat)
            {
                this.joueur.habilete.recuperer();
                Console.Clear();

                this.ui.afficherEntete();

                string[] nomCombatants = { " ", " ", " " };
                for (int i = 0; i < combatants.Length; i++)
                {
                    nomCombatants[i] = combatants[i].nom;
                }
                this.ui.afficherArene(nomCombatants);

                string[] stats = new string[1 + combatants.Length];
                stats[0] = this.joueur.enumererStats();
                for (int i = 1; i < combatants.Length + 1; i++)
                {
                    stats[i] = combatants[i - 1].enumererStats();
                }
                this.ui.afficherStats(stats);

                int action = this.ui.afficherMenuCombat(this.joueur.enumererActions().Split(','));

                nomCombatants = new string[combatants.Length];
                for (int i = 0; i < combatants.Length; i++)
                {
                    if (combatants[i].estVivant())
                    {
                        nomCombatants[i] = combatants[i].nom;
                    }
                    else
                    {
                        nomCombatants[i] = "mort";
                    }
                }
                int cible = this.ui.afficherMenuCible(nomCombatants);

                if (action == 0)
                {
                    combatants[cible].defendre(false, joueur.attaquer());
                }
                else if (action == 1)
                {
                    combatants[cible].defendre(true, joueur.habilete.executer(joueur.matt));
                }

                for (int i = 0; i < combatants.Length; i++)
                {
                    if (combatants[i].estVivant())
                    {
                        joueur.defendre(combatants[i].magique, combatants[i].attaquer());
                    }
                }

                bool ennemisMorts = true;
                for (int i = 0; i < combatants.Length; i++)
                {
                    if (combatants[i].estVivant())
                    {
                        ennemisMorts = false;
                    }
                }

                if (ennemisMorts || !this.joueur.estVivant())
                {
                    combat = false;
                }
            }

            if (this.joueur.estVivant())
            {
                this.etat = 2;
            }
            else
            {
                this.etat = 4;
            }
        }

        public void finale()
        {
            Console.Clear();
            this.ui.afficherStats(new string[1] { this.joueur.enumererStats() });
            if (this.ui.afficherMenuFin() == 1)
            {
                this.etat = 0;
            }
            else
            {
                this.etat = -1;
            }
        }
    }
}
