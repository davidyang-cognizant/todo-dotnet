import './App.css';
import Header from "./Components/Header"
import { Container } from 'react-bootstrap';
import { useEffect, useState } from 'react';
import { TodoTable } from './Components/TodoTable';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';
import { FilterBar } from './Components/FilterBar';

function App() {
  const [todoList, setTodoList] = useState([]);

  useEffect(() => {
    axios.get("https://localhost:5001/api/Todo")
      .then(res => {
        setTodoList(res.data)
      })
  }, [])

  const handleSubmit = (event, newTask) => {
    axios.post(`https://localhost:5001/api/Todo/add`, { task: newTask })
      .then(res => {
        setTodoList(prevState => [...prevState, res.data])
        toast("add successful", {
          position: "top-right"
        });
      })
  }

  const handleDelete = (id) => {
    axios.delete(`https://localhost:5001/api/Todo/delete/${id}`)
      .then(res => {
        setTodoList(prevState => prevState.filter(item => item.id !== id));
      })
  }

  const handleEdit = (e, taskToUpdate) => {
    axios.put(`https://localhost:5001/api/Todo/edit/${taskToUpdate.id}`, taskToUpdate)
      .then(res => {
        setTodoList(prevState => prevState.map(todo => {
          if (todo.id === taskToUpdate.id) {
            todo.task = taskToUpdate.task
          }
          return todo;
        }));
      })
  }

  return (
    <div className='app'>
      <Header handleSubmit={handleSubmit} />
      <div className='m-3'>      
        <FilterBar/>
      </div>
      <Container className='mt-4'>
        <TodoTable handleEdit={handleEdit} handleDelete={handleDelete} todoList={todoList} />
      </Container>
      <ToastContainer progressStyle={{ background: "#ede0c9" }} />
    </div>
  );
}

export default App;
