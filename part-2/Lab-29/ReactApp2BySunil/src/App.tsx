import { Route, Routes } from 'react-router-dom';
import './App.css';
import Footer from './components/Footer';
import Header from './components/Header';
import Home from './components/Home';
import Calculator from './components/Calculator';

function App() {
  return (
    <>
      <Header />
      <main className="px-8 min-h-20 py-4 text-center">
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/calculator" element={<Calculator />}></Route>
        </Routes>
      </main>
      <Footer />
    </>
  );
}

export default App;
