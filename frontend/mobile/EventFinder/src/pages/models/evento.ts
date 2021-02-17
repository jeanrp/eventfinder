export class Evento {
    id: string;
    nome: string;
    descricao: string;
    subDescricao: string;
    descPatrocinadores: string;
    dataHoraInicio: Date;
    dataHoraFim: Date;    
    categoriaId : string;
    situacao: string;
    empresaId: string;
    endereco: Endereco;
    atracoes: Atracao[];
    ingressos: Ingresso[];
    fotos: Foto[];     
}

export class Endereco {
    id: string;
    logradouro: string;
    numero: string;
    complemento: string;    
    bairro: string;
    cep: string;
    cidade: Cidade;
    eventoId: string;
    enderecoFormatado: string;
}

export class Estado {
    id: string;
    uf: string; 
}

export class Foto {
    id: string;
    imagem: string;
    imagemBase64: string;
    descricao : string;
    eventoId: string;
    empresaId: string;
    clienteId: string;
}
 
export class Categoria {
    id: string;
    classificacao: number; 
    descricao:string;
}

export class Cidade
{
    id: string;
    nome: string;
    estadoId: string;    
}


export class Atracao {
    id: string;
    nome: string;
    estilo: string; 
 }

 export class Ingresso {
    id: string;
    tipo: string;
    preco: string;
    lote: number;
 }

 