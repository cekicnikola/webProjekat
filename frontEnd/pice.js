export class Pice{
    constructor(naziv, cena, id){
        this.id = id;
        this.naziv = naziv;
        this.cena = cena;
        
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
   

}