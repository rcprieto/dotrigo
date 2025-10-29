export function converterStringParaData(dataString: string): Date {
  const [dia, mes, ano] = dataString.split('/');
  return new Date(Number(ano), Number(mes) - 1, Number(dia));
}

export function gerarAnosAnteriores(): number[] {
  const anoAtual = new Date().getFullYear() + 1;
  return Array(4)
    .fill(0)
    .map((_, i) => anoAtual - i);
}

export function converterDataParaISO8601(dataString: string) {
  // Tenta criar um objeto Date a partir da string
  let data: Date;

  // Verifica se a string está no formato DD/MM/YYYY
  if (dataString.toString().includes('/')) {
    const partes = dataString.split('/');
    const dia = parseInt(partes[0], 10);
    const mes = parseInt(partes[1], 10) - 1; // Meses em JavaScript começam em 0
    const ano = parseInt(partes[2], 10);
    data = new Date(ano, mes, dia);
  } else {
    // Tenta criar um Date a partir de outros formatos (como "Tue Oct 01 2024...")
    data = new Date(dataString);
  }

  // Verifica se a data é válida
  if (isNaN(data.getTime())) {
    throw new Error('Data inválida');
  }

  // Retorna a data no formato ISO 8601
  return data.toISOString();
}

export function converterParaSalvar(dataString: string) {
  const teste = String(dataString).indexOf('/');
  if (teste < 0) {
    return new Date(dataString).toLocaleDateString('pt-BR');
  } else return dataString;
}
