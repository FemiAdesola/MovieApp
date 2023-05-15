import React from 'react';
import Header from '../Header';
import { Outlet, useNavigate } from 'react-router-dom';
import Footer from '../Footer';
import FooterBottom from '../FooterBottom';

const Layout = () => {
  // const navigate = useNavigate();
  const isAdmin = true;
    return (
      <>
        <Header />
        <div className="container">{isAdmin && <Outlet /> }</div>
        <Footer />
        <div id="footer">{<FooterBottom />}</div>
      </>
    );
};

export default Layout;