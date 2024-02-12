import "./assets/global.css";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from "./components/Header";
import Input from "./components/Input";
import Tables from "./components/Tables";
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <div className="App">
      <Router>
        <Header />
        <div className="main-container">
          <Routes>
            <Route index element={<Input />} />
            <Route path="test" element={<Tables />} />
          </Routes>
        </div>
      </Router>
    </div>
  );
}

export default App;
