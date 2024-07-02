import PropTypes from "prop-types";
import { useState } from "react";
import { Table, Button, Form } from "react-bootstrap";

const DashBoardTable = ({ columns, data, onSave, onDelete }) => {
  const [editIndex, setEditIndex] = useState(null);
  const [editText, setEditText] = useState({});

  const handleEditClick = (index) => {
    setEditIndex(index);
    setEditText({ ...data[index] });
  };

  const handleSaveClick = (index) => {
    onSave(index, editText);
    setEditIndex(null);
  };

  const handleCancelClick = () => {
    setEditIndex(null);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setEditText((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const columnsMappedForHead = columns.map((column) => (
    <th key={column.accessor}>{column.Header}</th>
  ));

  const dataMapped = data.map((row, index) => (
    <tr key={row.id}>
      {columns.map((column) => (
        <td key={column.accessor}>
          {editIndex === index && column.editable ? (
            <Form.Control
              type={
                column.accessor === "date" || column.accessor === "time"
                  ? "datetime-local"
                  : "text"
              }
              name={column.accessor}
              value={editText[column.accessor]}
              onChange={handleInputChange}
            />
          ) : column.accessor === "password" ? (
            "********" // Muestra asteriscos si no está en modo edición
          ) : (
            row[column.accessor]
          )}
        </td>
      ))}
      <td>
        {editIndex === index ? (
          <>
            <Button variant="success" onClick={() => handleSaveClick(index)}>
              Guardar
            </Button>
            <Button variant="secondary" onClick={handleCancelClick}>
              Cancelar
            </Button>
          </>
        ) : (
          <>
            <Button variant="primary" onClick={() => handleEditClick(index)}>
              Editar
            </Button>
            <Button variant="danger" onClick={() => onDelete(row.id)}>
              Eliminar
            </Button>
          </>
        )}
      </td>
    </tr>
  ));

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          {columnsMappedForHead}
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>{dataMapped}</tbody>
    </Table>
  );
};

DashBoardTable.propTypes = {
  columns: PropTypes.array.isRequired,
  data: PropTypes.array.isRequired,
  onSave: PropTypes.func.isRequired,
  onCancel: PropTypes.func.isRequired,
  onDelete: PropTypes.func.isRequired,
};

export default DashBoardTable;
