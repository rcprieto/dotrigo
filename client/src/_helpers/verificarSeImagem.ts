export function verificarSeImagem(arquivo: string): boolean {
  if (!arquivo) {
    return false;
  }
  const extensoesImagem = ['.jpg', '.jpeg', '.png', '.gif', '.bmp', '.heic'];
  const nomeArquivoLower = arquivo.toLowerCase();

  return extensoesImagem.some((ext) => nomeArquivoLower.endsWith(ext)) || nomeArquivoLower.includes('https://s3');
}
