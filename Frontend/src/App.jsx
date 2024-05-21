import { useState } from 'react'
import { Router, Route, Routes } from "react-router-dom";
import './App.css'
import Header from './layouts/Header'
import LandingPage from './pages/LandingPage';
import Footer from './layouts/Footer';
import ProductPage from './pages/ProductPage';
import Productv2Page from './pages/Productv2Page';

function App() {
  return (
    <>
     <Header/>
     <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/product" element={<ProductPage />} />
          <Route path="/productv2" element={<Productv2Page />} />
     </Routes>
     <Footer/>
    </>
  )
}

export default App
