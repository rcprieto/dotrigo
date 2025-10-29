export function toFormData<T>(model: T): FormData {
  const formData = new FormData();

  // Percorre todas as chaves do objeto
  for (const key of Object.keys(model as any)) {
    const value = (model as any)[key];

    // Ignora valores nulos ou indefinidos para não enviar campos vazios desnecessários
    if (value !== null && value !== undefined) {
      // O append trata o File de forma diferente de outros tipos
      formData.append(key, value);
    }
  }

  return formData;
}
