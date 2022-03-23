import { Pice } from "./pice.js";

export class Sto{
    constructor(id, brojStola,meniRef, oznaka){
        this.id=id;
        this.brojStola=brojStola;
        this.kontejner=null;
        this.pica=[];
        this.oznaka=oznaka;
        this.meniRef=meniRef;
        this.kolicina=0;
    }

    ukloniPice(pice){
        this.pica.filter(p=>p.Naziv !== pice.Naziv &&
            p.Cena !== pice.Cena);
    }

    dodajPice(pice,kolicina){


        let p=this.pica.find(p=>p.naziv==pice.naziv);
        if(p != undefined){
            
            let t = document.querySelector(".Racun" + this.oznaka + "-" + this.brojStola);
            let str=t.innerHTML;
            let temp;
            
            temp=str.replace(pice.naziv + " - " + pice.cena + "  x  " + p.Kolicina +  "<br>","");
            

            p.Kolicina=p.Kolicina + kolicina;
           
        t.innerHTML=temp + pice.naziv + " - " + pice.cena + "  x  " + p.Kolicina +  "<br>"; 
        }else{
            
        //dodaj kolicinu i izmeni ovu fju
        pice.Kolicina=kolicina;
        
        this.pica.push(pice);
        let t = document.querySelector(".Racun" + this.oznaka + "-" + this.brojStola);
        t.innerHTML += pice.naziv + " - " + pice.cena + "  x  " + pice.Kolicina +  "<br>"; 
        }
        // treba update prikaz
        
    }

    plati(racun){
        let t = document.querySelector(".Racun" + this.oznaka + "-" + this.brojStola);
        t.innerHTML = "";
        
        let str = "";
        this.pica.forEach(  (pice) => {
            //suma+= parseInt(pice.cena);
            str+= pice.Naziv + " - " + pice.Cena +" x "+pice.Kolicina+ '\n';
        });
        str+= racun;
        alert(str);
        delete this.pica;
        this.pica = [];
    }
    crtajSto(host){
        
        this.kontejner = document.createElement("div");
        this.kontejner.classList.add("sto");
        host.appendChild(this.kontejner);

        const broj = document.createElement("label");
        broj.innerHTML = (this.brojStola+1);
        this.kontejner.appendChild(broj);

        const racun = document.createElement("p");

        let id = "Racun" + this.oznaka + "-" + this.brojStola; // za lociranje elementa na stranici
        racun.classList.add(id);

        this.kontejner.appendChild(racun);

        const forma = document.createElement("form");
        forma.classList.add("forma");
        this.kontejner.appendChild(forma);

        const sel = document.createElement("select");
        id = "naruciSelect" + this.oznaka + "-" + this.brojStola; // za lociranje elementa na stranici
        sel.classList.add(id);
        forma.appendChild(sel);

        const divKol=document.createElement("div");
        divKol.classList.add("divKolicine");
        forma.appendChild(divKol);

        const kolicinaLab=document.createElement("label");
        kolicinaLab.classList.add("kolicinaLabela");
        kolicinaLab.innerHTML="Kolicina:";
        divKol.appendChild(kolicinaLab);

        const kolicina=document.createElement("input");
        kolicina.type="number";
        id="kolicinaPica" + this.oznaka + "-" + this.brojStola;
        kolicina.classList.add(id);
        divKol.appendChild(kolicina);

        

        let stavka;

        this.meniRef.stavke.forEach( pice => {
            stavka = document.createElement("option");
            stavka.classList.add("stavkaMeni");

            stavka.value = pice.cena;
            stavka.name = pice.naziv;
            stavka.innerHTML = pice.naziv;
            sel.add(stavka);
        });

        sel.onclick=(event) => {
            let identity = ".btnNaruci" + this.oznaka + "-" + this.brojStola; // za lociranje elementa na stranici
            btnD = document.querySelector(identity);
            if(sel.selectedIndex >= 0){
                btnD.removeAttribute("disabled");
            }
            else{
                btnD.setAttribute('disabled', true);
            }
        }

        const btnDiv = document.createElement("div");
        btnDiv.classList.add("dugmadMeni");
        this.kontejner.appendChild(btnDiv);

        let btnD = document.createElement("button");
        btnD.classList.add("dugme");
        btnD.innerHTML = "Naruci";
        id = "btnNaruci" + this.oznaka + "-" + this.brojStola; // za lociranje elementa na stranici
        btnD.classList.add(id);
        btnD.setAttribute('disabled', true);

        btnD.onclick = (event) =>{
            let s = document.querySelector(".naruciSelect" + this.oznaka + "-" + this.brojStola); // za lociranje elementa na stranici
            let kol=document.querySelector(".kolicinaPica" + this.oznaka + "-" + this.brojStola);
            let piceid = this.meniRef.stavke[s.selectedIndex].id;
            let p=this.meniRef.stavke[s.selectedIndex];
            let pivoHrana=this.meniRef.stavke[s.selectedIndex].piceIliHrana;
            let kolPiva;
            let kolHrane;
            let picepostoji = 0;

            this.pica.forEach( pice => {
                if(pice.id == piceid && picepostoji == 0){
                    // pice postoji, treba azurirati broj pica u bazi
                    fetch("https://localhost:5001/Narudzbina/DodajJosPiva/" + this.id + "/" + piceid + "/" +parseInt(kol.value), {
                        method: "PUT",
                        headers: {
                            "Content-Type": "application/json"
                        }
                        })
                        .then(p => {
                            if (p.ok) {
                                alert("Jos jednom ste uspesno narucili.");
                                p.Kolicina=p.Kolicina + parseInt(kol.value);

                            }
                            else if(p.status == 406){
                                alert("Pice na stolu nije nadjeno.");
                            }
                            else{
                                alert("Greska prilikom upisa");
                            }
                        });
                    picepostoji++;
                }
            });
            if(picepostoji == 0){
                // ako pice ne postoji na datom stolu, treba ga dodati
                if(pivoHrana === true){
                    kolPiva=parseInt(kol.value);
                    kolHrane=parseInt(0);

                }
                else{
                    kolHrane=parseInt(kol.value);
                    kolPiva=parseInt(0);

                }
                fetch("https://localhost:5001/Narudzbina/Naruci/" + this.id + "/" + piceid, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        "piceID": piceid,
                        "kolicinaPiva": kolPiva,
                        "kolicinaHrane": kolHrane,
                        "doneta":false,
                        "stoID": this.id
                    })
                })
                .then(p => {
                    if (p.ok) {
                        alert("Pice na stolu.");

                    }
                    else {
                        alert("Greška prilikom upisa.");
                    }
                });
            }
            
            this.dodajPice(this.meniRef.stavke[s.selectedIndex], parseInt(kol.value));
            let b = document.querySelector(".btnPlati" + this.oznaka + "-" + this.brojStola);
            b.removeAttribute('disabled');
            b.parentNode.parentNode.style.backgroundColor = "rgb(252, 193, 137)";
        }
        btnDiv.appendChild(btnD);

        let btnP = document.createElement("button");
        btnP.innerHTML = "Plati";
        id = "btnPlati" + this.oznaka + "-" + this.brojStola;
        btnP.classList.add(id);
        btnP.setAttribute('disabled', true);
        btnP.classList.add("dugme");

        btnP.onclick = (event) => {
            fetch("https://localhost:5001/Narudzbina/Naplati/" + this.id, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify ({
                    
                })
            })
            .then(p => {
                if(p.ok){ 

                    
                    /* Ako je brisanje pica sa stola u bazi proslo ok,
                    azurira se prikaz na stranici. */
                    p.text().then(p=>{
                        let racun="";
                        racun+=p;
                        
                     this.plati(racun);
                    
                     btnP.setAttribute('disabled', true);
                     btnP.parentNode.parentNode.style.backgroundColor = "rgb(140, 194, 255)";
                        });
                    
                   
                }
                else alert("Greska prilikom placanja.");
            }
            );
        }
        btnDiv.appendChild(btnP);
        

        fetch("https://localhost:5001/Narudzbina/PregledNarudzbina/" + this.id).then(p => {
                p.json().then(data => {

                    /* Za svako pice koje je prethodno naruceno za sto,
                    dodaje se ispis na stolu onoliko puta koliko je to pice naruceno. */
   
                    data.forEach(naruceno => {
                        let temp;
                        var kolicina;

                        this.meniRef.stavke.forEach(s => {
                            // prvo se locira pice na meniju, zbog optimalnosti
                            let x =naruceno.pivoHrana;
                            if(s.id ==x.id){
                               
                                
                               temp = s;
                                
                                if(naruceno.kolicinaHrane > 0){
                                    kolicina=naruceno.kolicinaHrane;
                                    
                                }
                                else{
                                kolicina=naruceno.kolicinaPiva;
                                }
                                
                               // kolicina=VratiKolicinu(naruceno);
                                
                            }
                        });

                        //for(let i = 0; i < kolicina; i++)
                            this.pica.push(new Pice(temp.naziv, temp.cena, temp.id,temp.pivoHrana,kolicina));
                            
                            racun.innerHTML += temp.naziv + " - " + temp.cena + " x "+ kolicina + "<br>";
                        
                        let b = document.querySelector(".btnPlati" + this.oznaka + "-" + this.brojStola);
                        b.removeAttribute('disabled');
                        b.parentNode.parentNode.style.backgroundColor = "rgb(252, 193, 137)";
                    });
                }); 
            });

    }
    addStavkaMeniSto(){
        let s = document.querySelector(".naruciSelect" + this.oznaka + "-" + this.brojStola);
        let stavka = document.createElement("option");

        let naz = document.querySelector(".nazivPica" + this.meniRef.id);
        let c = document.querySelector(".cenaPica" + this.meniRef.id);
        stavka.value = c.value;
        stavka.name = naz.value;
        stavka.innerHTML = naz.value;
        stavka.classList.add("stavkaMeni");
        s.appendChild(stavka);
    }

    removeStavkaMeniSto(index){
        let s = document.querySelector(".naruciSelect" + this.oznaka + "-" + this.brojStola);
        s.options[index] = null;
        delete s.options[index];
    }

    updateStavkaMeniSto(index, stavka){
        let s = document.querySelector(".naruciSelect" + this.oznaka + "-" + this.brojStola);
        s.options[index].name = stavka.name;
        s.options[index].value = stavka.value;
        s.options[index].innerHTML = stavka.innerHTML;
    }
    vratiKolicinu(narudzbina){
        if(narudzbina.KolicinaPiva > 0){
            return narudzbina.KolicinaPiva;
        }
        else{
            return narudzbina.KolicinaHrane;
        }      
        
    }
}