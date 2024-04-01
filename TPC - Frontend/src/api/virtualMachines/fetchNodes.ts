import api from "@api/api";

export async function FetchNodeNames() {
  const result = await api.get<Array<string>>("/nodes");

  console.log(result.data);

  return result.data;
}
