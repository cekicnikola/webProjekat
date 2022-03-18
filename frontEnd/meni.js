import { Pice } from "./pice.js";

export class Meni{

    constructor(k, m){

        this.id = m.id;

        this.kontejner = null;

        this.pivnicaRef = k;

        this.stavke = [];

        m.stavke.forEach(pice => {
            const pivoHrana = new Pice(pice.naziv, pice.cena, pice.id); 
            this.stavke.push(pivoHrana);
        });
    }

    dodajStavku(){

        let klasa = ".nazivPica" + this.id;
        let naz = document.querySelector(klasa);

        klasa = ".cenaPica" + this.id;
        let cena = document.querySelector(klasa);

        let l = document.querySelector(".stavkeSelect"+this.id);

        let index = this.stavke.length + 1;

        fetch("https://localhost:5001/PivoHrana/DodajPivo/" + this.id, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    "naziv": naz.value,
                    "cena": cena.value,
                    "piceIlihrana":true
                })
            }).then(p => {
                if (p.ok) {

                    /* Ako je pice uspesno upisano u bazu, 
                    potrebno je dodati novu stavku u meniju, 
                    kao i azurirati meni na svakom stolu  */

                    let stavka = document.createElement("option");
                    stavka.classList.add("stavkaMeni");
                    stavka.value = cena.value;
                    stavka.name = naz.value;
                    stavka.innerHTML = naz.value;
                    l.add(stavka);

                    /* baza generise ID novog pica, ali taj ID 
                    je potreban za dodavanje pica na sto. Potrebno je 
                    preuzeti ID iz baze.*/
                    
                    p.json().then(p => {
                        let s = new Pice(naz.value,cena.value, p);
                        this.stavke.push(s);
                        
                    }); 
                }
                else if (p.status == 406) {
                    // server vraca kod 406 ako parametri pica nisu regularni
                    alert("Cena pica ne sme da bude negativna, i pice treba da ima naziv!");
                }
                else {
                    alert("Greška prilikom upisa.");
                }
            }).catch(p => {
                alert("Greška prilikom upisa.");
        });
    }

    obrisiStavku(){
        let lista = document.querySelector(".stavkeSelect"+this.id);
        let index = lista.options.selectedIndex;
        let stavka = this.stavke[index];

        fetch("https://localhost:5001/PivoHrana/IzbrisiPivoHranu/" + stavka.id, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify ({
                   
                })
            }).then(p => {
                if (p.ok){
                    /* Ako je brisanje u bazi proslo ok, potrebno je
                    azurirati meni pivnice, kao i meni na svakom stolu. */

                    lista.removeChild(lista.options[index]);
                    this.stavke = this.stavke.filter(s=>s.id!==stavka.id);

                   
                }
                else
                {
                    alert("Doslo je do greske prilikom brisanja");
                }
        });   
    }

    izmeniStavku(index){
        let naz = document.querySelector(".nazivPica"+this.id);
        let cena = document.querySelector(".cenaPica"+this.id);

        let lista = document.querySelector(".stavkeSelect"+this.id);
        let stavka = lista.options[index];

        let s = (this.stavke[index]);

        fetch("https://localhost:5001/PivoHrana/IzmeniPivo/" + s.id, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify ({
                    "naziv": naz.value,
                    "cena": cena.value
                })
            }).then(p => {
                if (p.ok){
                    // ako je izmena u bazi prosla ok, potrebno je azurirati prikaz
                    s.Naziv = lista.options[index].name = naz.value;
                    s.Cena = lista.options[index].value = cena.value;
                    lista.options[index].innerHTML = naz.value;

                    
                }
                else{
                    alert("Doslo je do greske prilikom izmena.");
                }
        });
    }

    prikaziMeni(host){
        if(!host){
            throw new Exception("Host element za meni ne postoji!");
        }

        this.kontejner = document.createElement("div");
        let klasa = "meni" + this.id; // za lociranje elementa na stranici
        this.kontejner.classList.add(klasa);
        this.kontejner.classList.add("meni");
        host.appendChild(this.kontejner);
        let meniTekst = document.createElement("h2");
        meniTekst.innerHTML = "Meni";
        this.kontejner.appendChild(meniTekst);

        this.crtajFormu(this.kontejner);
    }

    crtajFormu(host){
        if(!host){
            throw new Exception("Host element za stavke menija ne postoji!");
        }

        const forma = document.createElement("form");
        forma.classList.add("forma");
        host.appendChild(forma);

        const sel = document.createElement("select");
        sel.classList.add("stavkaMeni");
        sel.classList.add("stavkeSelect"+this.id); // za lociranje elementa na stranici
        sel.setAttribute('multiple', true);
        sel.size = 10;
        forma.appendChild(sel);

        let stavka;

        this.stavke.forEach( pice => {
            stavka = document.createElement("option");
            stavka.classList.add("stavkaMeni");

            stavka.value = pice.cena;
            stavka.name = pice.naziv;
            stavka.innerHTML = pice.naziv;
            sel.add(stavka);
        });

        // pri odabiru stavke, odgovarajuca polja dobijaju adekvatne vrednosti
        sel.onchange=(event) => {
            let i = sel.selectedIndex;
            if(i >= 0) {
                let naz = document.querySelector(".nazivPica"+this.id);
                naz.value = sel.options[i].name;
                naz = document.querySelector(".cenaPica"+this.id);
                naz.value = sel.options[i].value;
            }
        }



        /* Naziv i cena stavke */

        let p = document.createElement("label");
        p.name = "naziv"
        p.innerHTML = "Naziv: ";
        forma.appendChild(p);

        let inp = document.createElement("input");
        inp.classList.add("polje");
        inp.type = "text";
        inp.name = "naziv";
        let id = "nazivPica" + this.id; // za lociranje elementa na stranici
        inp.classList.add(id);
        
        p.appendChild(inp);

        p = document.createElement("label");
        p.name = "cena";
        p.innerHTML = "Cena: ";
        forma.appendChild(p);

        inp = document.createElement("input");
        inp.classList.add("polje");
        inp.type = "number";
        inp.name = "cena";
        id = "cenaPica" + this.id; // za lociranje elementa na stranici
        inp.classList.add(id);
        p.appendChild(inp);
     

        /* Dugmad za dodavanje, izmenu i brisanje stavki iz menija */

        const btnDodaj = document.createElement("input");
        btnDodaj.classList.add("dugme");
        btnDodaj.type = "button";
        btnDodaj.value = "Dodaj stavku";    

        btnDodaj.onclick=(event) => {
            this.dodajStavku();
        }
        forma.appendChild(btnDodaj);

        const btnIzmeni = document.createElement("input");
        btnIzmeni.classList.add("dugme");
        btnIzmeni.type = "button";
        btnIzmeni.value = "Izmeni stavku";
        forma.appendChild(btnIzmeni);

        btnIzmeni.onclick=(event) => {
            let lista = document.querySelector(".stavkeSelect"+this.id);
            let i = lista.options.selectedIndex;
            this.izmeniStavku(i);
        }

        const btnObrisi = document.createElement("input");
        btnObrisi.classList.add("dugme");
        btnObrisi.type = "button";
        btnObrisi.value = "Obrisi stavku";
        forma.appendChild(btnObrisi);

        btnObrisi.onclick=(event) => {
            this.obrisiStavku();
        }

    }
}