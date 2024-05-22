import { useState } from 'react'
import { Router, Route, Routes } from "react-router-dom";
import './App.css'
import Header from './layouts/Header'
import LandingPage from './pages/LandingPage';
import Footer from './layouts/Footer';

function App() {
  return (
    <>
     <Header/>
     <Routes>
          <Route path="/" element={<LandingPage />} />
     </Routes>
     <Footer/>
    </>
  )
}

export default App
