import PropTypes from "prop-types";

const ManagementTable = ({ columns, data, onEdit, onDelete }) => {
  const columnsMappedForHead = columns.map((column) => (
    <th key={column.accessor}>{column.Header}</th>
  ));

  const dataMapped = data.map((row) => (
    <tr key={row.id}>
      {columns.map((column) => (
        <td key={column.accessor}>{row[column.accessor]}</td>
      ))}
      <td>
        <button onClick={() => onEdit(row)}>Edit</button>
        <button onClick={() => onDelete(row.id)}>Delete</button>
      </td>
    </tr>
  ));

  return (
    <table>
      <thead>
        <tr>
          {columnsMappedForHead}
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>{dataMapped}</tbody>
    </table>
  );
};

ManagementTable.propTypes = {
  columns: PropTypes.array,
  data: PropTypes.object,
  onEdit: PropTypes.func,
  onDelete: PropTypes.func,
};

export default ManagementTable;
