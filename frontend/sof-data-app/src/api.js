const API_BASE =
  process.env.REACT_APP_API_BASE || 'http://4.210.65.98:5000';

export async function fetchTags({
  page = 1,
  pageSize = 10,
  sortField = 'Id',
  descending = false,
}) {
  const params = new URLSearchParams({
    page: String(page),
    pageSize: String(pageSize),
    sortBy: capitalizeFirstLetter(sortField),
    descending: String(descending),
  });

  const url = `${API_BASE}/StackOverFlow/Tags?${params.toString()}`;
  const res = await fetch(url);

  if (!res.ok) {
    const txt = await res.text();
    throw new Error(`Błąd HTTP ${res.status}: ${txt}`);
  }

  return await res.json();
}

function capitalizeFirstLetter(str) {
  if (!str) return '';
  return str.charAt(0).toUpperCase() + str.slice(1);
}
