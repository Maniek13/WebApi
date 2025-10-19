import React, { useEffect, useState, useCallback } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Paginator } from 'primereact/paginator';
import { fetchTags } from '../api';

export default function TagsTable() {
  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(false);
  const [totalRecords, setTotalRecords] = useState(0);

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [sortField, setSortField] = useState('Id');
  const [sortOrder, setSortOrder] = useState(1); 

  const loadData = useCallback(async () => {
    setLoading(true);
    try {
      const resp = await fetchTags({
        page,
        pageSize,
        sortField,
        descending: sortOrder === -1,
      });
      setItems(resp.items || []);
      setTotalRecords(resp.totalCount || 0);
    } catch (err) {
      console.error(err);
      setItems([]);
      setTotalRecords(0);
    } finally {
      setLoading(false);
    }
  }, [page, pageSize, sortField, sortOrder]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const onPageChange = (event) => {
    const newPage = Math.floor(event.first / event.rows) + 1;
    setPage(newPage);
    setPageSize(event.rows);
  };

const onSort = (e) => {
  if (e.sortField) {
    if (sortField === e.sortField) {
      setSortOrder(sortOrder === 1 ? -1 : 1);
    } else {
      setSortField(e.sortField);
      setSortOrder(1);
    }
  }
  setPage(1);
};

  return (
    <div className="app-container">
      <h1 className="text-2xl font-semibold mb-4">StackOverFlow Tags</h1>

      <DataTable
        value={items}
        loading={loading}
        responsiveLayout="scroll"
        sortMode="single"
        onSort={onSort}
      >
        <Column field="name" header="Name" sortable />
        <Column field="count" header="Count" sortable />
        <Column
          field="participation"
          header="Participation"
          sortable
          body={(rowData) => rowData.participation != null ? (rowData.participation).toFixed(2) + "%" : ""}
        />
      </DataTable>

      <div className="mt-4">
        <Paginator
          first={(page - 1) * pageSize}
          rows={pageSize}
          totalRecords={totalRecords}
          onPageChange={onPageChange}
          template="PrevPageLink PageLinks NextPageLink RowsPerPageDropdown"
          rowsPerPageOptions={[5, 10, 20, 50]}
        />
      </div>
    </div>
  );
}
