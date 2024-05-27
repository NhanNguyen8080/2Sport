import { useState } from 'react'
import { Router, Route, Routes } from "react-router-dom";
import './App.css'
import Header from './layouts/Header'
import LandingPage from './pages/LandingPage';
import ProductPage from './pages/ProductPage';
import Productv2Page from './pages/Productv2Page';
import Footer from './layouts/Footer';
import ManageAccount from './pages/ManageAccount';
import { BreadcrumbsDefault } from './layouts/BreadcrumbsDefault';

function App() {
  return (
    <>
     <Header/>
     <div className="pt-28">
      {/* <BreadcrumbsDefault/> */}
     <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/product" element={<ProductPage />} />
          <Route path="/productv2" element={<Productv2Page />} />
     </Routes>
     </div>
     <Footer/>
    </>
  )
}

export default App
