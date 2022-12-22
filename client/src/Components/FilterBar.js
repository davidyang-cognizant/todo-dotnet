import { Card, Row, Col, Nav, Navbar, NavDropdown } from "react-bootstrap";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCircleCheck, faFilter, faArrowUpShortWide } from '@fortawesome/free-solid-svg-icons'
export const FilterBar = (props) => {
    return (
        <Card className="p-0 m-0">
            <Card.Body className="p-1">
                <div className="d-flex justify-content-between">
                    <span className="m-0">Created recently</span>
                    <Navbar className="p-0">
                        <Navbar.Collapse className="p-0" id="basic-navbar-nav">
                            <Nav className=" p-0 me-auto">
                                <Nav.Link className="p-0 me-4" href="#home">
                                    <FontAwesomeIcon className="me-2" icon={faCircleCheck} />All Tasks
                                </Nav.Link>
                                <Nav.Link className="p-0 me-4" href="#link">
                                    <FontAwesomeIcon className="me-2" icon={faFilter} />Filter
                                </Nav.Link>
                                <Nav.Link className="p-0 me-4" href="#link">
                                    <FontAwesomeIcon className="me-2" icon={faArrowUpShortWide} />Sort
                                </Nav.Link>
                            </Nav>
                        </Navbar.Collapse>
                    </Navbar>
                    {/* <Row className="w-25">
                            <Col>
                               
                            </Col>
                            <Col>
                                <FontAwesomeIcon className="me-2" icon={faFilter} />
                                Filter
                            </Col>
                        </Row> */}
                </div>
            </Card.Body>
        </Card>
    )
};
