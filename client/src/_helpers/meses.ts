export const MESES = [
  { valor: 1, nome: 'Janeiro', nomeAbreviado: 'Jan' },
  { valor: 2, nome: 'Fevereiro', nomeAbreviado: 'Fev' },
  { valor: 3, nome: 'Março', nomeAbreviado: 'Mar' },
  { valor: 4, nome: 'Abril', nomeAbreviado: 'Abr' },
  { valor: 5, nome: 'Maio', nomeAbreviado: 'Mai' },
  { valor: 6, nome: 'Junho', nomeAbreviado: 'Jun' },
  { valor: 7, nome: 'Julho', nomeAbreviado: 'Jul' },
  { valor: 8, nome: 'Agosto', nomeAbreviado: 'Ago' },
  { valor: 9, nome: 'Setembro', nomeAbreviado: 'Set' },
  { valor: 10, nome: 'Outubro', nomeAbreviado: 'Out' },
  { valor: 11, nome: 'Novembro', nomeAbreviado: 'Nov' },
  { valor: 12, nome: 'Dezembro', nomeAbreviado: 'Dez' },
  { valor: 13, nome: 'Ano', nomeAbreviado: 'Ano' },
];

export function retornaNomeMes(mes: number, abreviado: boolean = false): string {
  if (!abreviado) return MESES.filter((c) => c.valor == mes)[0].nome;

  return MESES.filter((c) => c.valor == mes)[0].nomeAbreviado;
}
