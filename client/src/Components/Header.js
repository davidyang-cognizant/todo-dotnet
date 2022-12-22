import { Navbar } from 'react-bootstrap'
import "../CSS/Header.css"
import { TodoForm } from './TodoForm';
const Header = (props) => {
    return (
        <div className={"header"}>
            <Navbar className={"header"} expand="lg">
            <Navbar.Brand className={"ms-2"} href="#home">todoodoo</Navbar.Brand>
            {/* <Navbar.Toggle aria-controls="basic-navbar-nav" /> */}
            </Navbar>
            <TodoForm handleSubmit={props.handleSubmit} />
        </div>
    )
}

export default Header;