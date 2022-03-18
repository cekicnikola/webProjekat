import {Pivnica} from "./pivnica.js"
/*export class Sto
{
    constructor ( id, brojStola){
        this.id=id;
        this.brojStola=brojStola;
    }
    
}*/
/*fetch("https://localhost:5001/PivoHrana/PreuzmiSveStavke")
.then(p=>{
    p.json().then(stolovi=>{
        stolovi.forEach(sto =>{
        console.log(sto);
        
        });
    })
})*/
let pivnica;
fetch("https://localhost:5001/Pivnica/PreuzmiPivnice")
.then(p => {
                p.json().then(data => {
                    data.forEach(p => {
                        console.log(p);
                        pivnica= new Pivnica(p);
                        pivnica.crtajPivnicu(document.body);
                    });
                }); 
            });
