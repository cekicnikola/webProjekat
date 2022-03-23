export class Pice{
    constructor(naziv, cena, id, piceIliHrana, kolicina){
        this.id = id;
        this.naziv = naziv;
        this.cena = cena;
        this.piceIliHrana=piceIliHrana;
        this.kolicina=kolicina;
        
    }

    get Naziv(){
        return this.naziv;
    }
    set Naziv(value){
        this.naziv = value;
    }

    get Cena(){
        return this.cena;
    }
    set Cena(value){
        this.cena = value;
    }
    get PiceIliHrana(){
        return this.piceIliHrana;
    }
    set PiceIliHrana(value){
        this.piceIliHrana= value;
    }
    get Kolicina(){
        return this.kolicina
    }
    set Kolicina(value){
        this.kolicina= value;
    }
   

}