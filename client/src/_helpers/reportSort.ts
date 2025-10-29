export function getNestedValue(obj: any, path: string) {
  return path.split('.').reduce((acc, part) => {
    if (acc && acc[part] !== undefined && acc[part] !== null) {
      return acc[part];
    }
    return undefined; // Se não existir, retorna undefined
  }, obj);
}
