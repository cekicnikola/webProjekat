import { Sto } from "./sto.js";
import { Meni } from "./meni.js";

export class Pivnica{
    constructor(p) 
    {
        this.id=p.id;
        this.naziv=p.naziv;
        this.brojStolova=parseInt(p.brojStolova);
        this.kontejner=null;
        this.meni=new Meni(this, p.meni);
        this.stolovi=[];
        for (let i=0;i<this.brojStolova;i++){
            this.stolovi.push(new Sto(p.stolovi[i].id, i, this.id));
        }
        
    }
    crtajPivnicu(host){
        let naslov=document.createElement("h1");
        naslov.classList.add("naslov");
        naslov.innerHTML=this.naziv;
        host.appendChild(naslov);
        this.kontejner=document.createElement("div");
        this.kontejner.classList.add("pivnica");
        host.appendChild(this.kontejner);

        this.meni.prikaziMeni(this.kontejner);
        const sala=document.createElement("div");
        sala.classList.add("salaStolovi");
        this.kontejner.appendChild(sala);

        /*this.meni.prikaziMeni(this.kontejner);
        const sala = document.createElement("div");
        sala.classList.add("salaStolovi");
        this.kontejner.appendChild(sala);*/

        /*this.stolovi.forEach((sto)=>{
            sto.crtajSto(sala);
        });*/

    }
}