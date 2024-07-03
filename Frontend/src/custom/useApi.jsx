import { useState, useEffect, useCallback } from "react";
import { API_BASE_URL } from "../api";

const useApi = (initialEntity) => {
  const [entity, setEntity] = useState(initialEntity);
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const response = await fetch(`${API_BASE_URL}/${entity}`);
      if (!response.ok) {
        throw new Error("El servicio está caído, intente más tarde");
      }
      const result = await response.json();
      setData(result);
      setLoading(false);
    } catch (err) {
      setError(err.message);
      setLoading(false);
    }
  }, [entity]);

  const createEntity = useCallback(
    async (newEntity) => {
      try {
        const response = await fetch(`${API_BASE_URL}/${entity}`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(newEntity),
        });
        if (!response.ok) {
          throw new Error(`Error al crear ${entity}`);
        }
        fetchData();
      } catch (err) {
        setError(err.message);
      }
    },
    [entity, fetchData]
  );

  const updateEntity = useCallback(
    async (id, updatedEntity) => {
      try {
        const response = await fetch(`${API_BASE_URL}/${entity}/${id}`, {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(updatedEntity),
        });
        if (!response.ok) {
          throw new Error(`Error al actualizar ${entity}`);
        }
        fetchData();
      } catch (err) {
        setError(err.message);
      }
    },
    [entity, fetchData]
  );

  const deleteEntity = useCallback(
    async (id) => {
      try {
        const response = await fetch(`${API_BASE_URL}/${entity}/${id}`, {
          method: "DELETE",
        });
        if (!response.ok) {
          throw new Error(`Error al eliminar ${entity}`);
        }
        fetchData();
      } catch (err) {
        setError(err.message);
      }
    },
    [entity, fetchData]
  );

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  return {
    data,
    loading,
    error,
    setEntity,
    createEntity,
    updateEntity,
    deleteEntity,
  };
};

export default useApi;
