import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faStar,
  faCircle,
  faArrowLeft,
  faArrowRight,
} from "@fortawesome/free-solid-svg-icons";
import React, { useState } from "react";
import {
  faFacebook,
  faInstagram,
  faTwitter,
} from "@fortawesome/free-brands-svg-icons";
import CountUp from "react-countup";

const startCounting = true;

const members = [
  {
    name: "Nguyễn Quốc Nhân",
    specialisations: "leader, dev",
    image: "/assets/images/aboutus/Avarta1.png",
  },
  {
    name: "Nguyễn Tuấn Vũ",
    specialisations: "dev",
    image: "/assets/images/aboutus/Avarta2.png",
  },
  {
    name: "Dương Thị Trúc Linh",
    specialisations: "dev",
    image: "/assets/images/aboutus/Avatar3.png",
  },
  {
    name: "Hà Thị Phương Thảo",
    specialisations: "dev",
    image: "/assets/images/aboutus/Avarta1.png",
  },
  {
    name: "Võ Nguyễn Hoàng Quân",
    specialisations: "marketing",
    image: "/assets/images/aboutus/Avarta2.png",
  },
  {
    name: "Võ Hoài Nhật",
    specialisations: "marketing",
    image: "/assets/images/aboutus/Avatar3.png",
  },
  {
    name: "Hà Anh Tài",
    specialisations: "marketing",
    image: "/assets/images/aboutus/Avarta1.png",
  },
  {
    name: "Lê Thanh Minh Nhật",
    specialisations: "marketing",
    image: "/assets/images/aboutus/Avarta2.png",
  },
];

const AboutUs = () => {
  const [currentIndex, setCurrentIndex] = useState(0);

  const handlePrev = () => {
    setCurrentIndex((prevIndex) => (prevIndex > 0 ? prevIndex - 1 : 0));
  };

  const handleNext = () => {
    setCurrentIndex((prevIndex) =>
      prevIndex < members.length - 3 ? prevIndex + 1 : 0
    );
  };
  return (
    <div className="relative mb-[4%]">
      <div className="flex justify-center items-center relative">
        <img
          src="/assets/images/aboutus/AboutUs.png"
          alt="contactUs"
          className="mx-auto h-[550px] w-[90%]"
        />
        <h1 className="text-[40px] py-10 text-white font-rubikmonoone font-bold absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 text-wrap text-center transition duration-300 ease-in-out transform hover:scale-110">
          Score Big Savings, Play Even Harder: Unleash Your Sporting Spirit with
          Affordable Second-Hand Finds!
        </h1>
        {/* <div className="flex w-3/4 justify-around bg-white border-zinc-600 border-2 absolute mt-[30%] drop-shadow-lg py-10">
      <div className="text-center">
        <h3 className="mt-4 text-[50px] font-bold">
          {startCounting && <CountUp end={50} duration={7} />}+
        </h3>
        <p className="mt-2 text-gray-400 text-xl">Year of Experience</p>
      </div>
      <div className="w-px bg-gray-300"></div>
      <div className="text-center">
        <h3 className="mt-4 text-[50px] font-bold">
          {startCounting && <CountUp end={633} duration={5} />}+
        </h3>
        <p className="mt-2 text-gray-400 text-xl">Happy Clients</p>
      </div>
      <div className="w-px bg-gray-300"></div>
      <div className="text-center">
        <h3 className="mt-4 text-[50px] font-bold">
          {startCounting && <CountUp end={150} duration={6} />}+
        </h3>
        <p className="mt-2 text-gray-400 text-xl">Expert Trainers</p>
      </div>
      <div className="w-px bg-gray-300"></div>
      <div className="text-center">
        <h3 className="mt-4 text-[50px] font-bold">
          {startCounting && <CountUp end={500} duration={5} />}k
        </h3>
        <p className="mt-2 text-gray-400 text-xl">Instagram followers</p>
      </div>
    </div> */}
      </div>

      <div className="mt-44 mx-[20%] flex">
        <div className="w-1/2 justify-center flex flex-col ml-10 mr-10">
          <div className="mb-10">
            <h2 className="text-[15px]  text-[#524FF5]">WELCOME</h2>
            <h1 className="text-[35px] font-bold text-black">2Sport</h1>
            <p className="text-[15px] text-[#6A6A6A] mt-3 text-wrap">
              2Sport is always the address providing hot secondhand products,
              including the world's leading brands such as Yonex, Nike, Adidas,
              Jordan, Jogarbola... at cheap prices, suitable for your pocket. In
              addition, 2Sport is also willing to buy, sell and consign limited
              edition products for collectors.
            </p>
          </div>
          <div className="bg-[#EEEEEE] p-[10%]">
            <h1 className="text-[35px] font-bold text-black">CORE VALUES:</h1>
            <p className="text-[15px] text-[#6A6A6A] mt-3 mb-5">
              2Sport is committed to bringing convenience and cost-effectiveness
              to sports lovers, by providing a trustworthy platform to shop for
              quality and valuable secondhand sportswear. With the name 2-sport,
              a new brand but with a great ambition is to bring customers the
              best service and experience possible, with the motto of creating
              convenience and saving costs effectively. efficiency for customers
              and create maximum credibility to increase the rate of customers
              returning to the store next time.
            </p>
            <img
              src="/assets/images/aboutus/about-us-1.jpg"
              alt="contactUs"
              className=""
            />
          </div>
        </div>

        <div className="w-1/2 mx-5">
          <div className="relative mb-10">
            <img
              src="/assets/images/aboutus/about-us-2.png"
              alt="contactUs"
              className="filter duotone-c86409-ffffff"
            />
            <div className="absolute bottom-12 left-8 transform translate-y-1/4 text-white">
              <h2 className="text-2xl font-bold">Our Mission</h2>
              <p className="text-lg mt-3 text-left">
                "2SPORT is committed to bringing the best quality products and
                services to sports players to improve their own health."
              </p>
            </div>
          </div>
          <div className="bg-[#B8DDEB] p-[10%]">
            <h1 className="font-bold text-[25px] text-black">VISION</h1>
            <p className="text-[15px] text-black mt-2">
              <FontAwesomeIcon
                icon={faCircle}
                className="text-black text-[10px] mr-1"
              />
              "Becoming the largest secondhand sportswear retailer in Vietnam".
            </p>
            <p className="text-[15px] text-black mt-2">
              <FontAwesomeIcon
                icon={faCircle}
                className="text-black text-[10px] mr-1"
              />
              Become the country's leading unit in the field of secondhand
              sportswear retail, helping to improve public health and protect
              the environment.
            </p>
            <p className="text-[15px] text-black mt-2">
              <FontAwesomeIcon
                icon={faCircle}
                className="text-black text-[10px] mr-1"
              />
              Bringing high quality products, suitable to the needs of
              Vietnamese people.
            </p>
            <p className="text-[15px] text-black mt-2 mb-3">
              <FontAwesomeIcon
                icon={faCircle}
                className="text-black text-[10px] mr-1"
              />
              Build a wide store system throughout 64 provinces and cities.
            </p>
          </div>
        </div>
      </div>

      <div className="flex flex-col justify-center items-center text-2xl font-bold mt-20">
        <h2 className="text-[#524FF5] justify-center font-bold">OUR MEMBERS</h2>
        <h1 className="text-[35px] font-bold text-black mt-5 mb-5">
          Bring best to you
        </h1>
      </div>
      <div className="relative px-20">
        <div className="overflow-hidden">
          <div
            className="flex transition-transform duration-500 ease-in-out"
            style={{ transform: `translateX(-${currentIndex * 33.33}%)` }}
          >
            {members.map((member, index) => (
              <div key={index} className="w-1/3 mr-5 flex-shrink-0">
                <img src={member.image} alt="contactUs" className="" />
                <div className="bg-[#1B2129] flex flex-col justify-between px-5">
                  <div className="text-xl font-thin justify-between flex mt-5">
                    <h2 className="text-white">{member.name}</h2>  
                    <div className="flex items-center ml-[40%]">
                      {/* <FontAwesomeIcon
                        icon={faStar}
                        className="text-yellow-500"
                      /> */}
                      {/* <p className="text-white ml-2">/5</p> */}
                    </div>
                  </div>
                  <div className="h-px bg-white mt-5"></div>
                  <h2 className="text-white font-thin mt-5 text-xl">
                    Specialisations:
                  </h2>
                  <p className="text-[#6A6A6A] text-lg font-thin">
                    {/* {member.specialisations} */}
                    <p className="text-white"> Nhóm trưởng,  lập trình viên backend</p>
                  </p>
                  <div className="my-5">
                    <FontAwesomeIcon
                      icon={faInstagram}
                      className="text-white"
                    />
                    <FontAwesomeIcon
                      icon={faFacebook}
                      className="pl-3 text-white"
                    />
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
        <button
          onClick={handlePrev}
          className="absolute h-full w-1/12 left-20 top-1/2 transform -translate-y-1/2 bg-gradient-to-r from-white/80"
        >
          <FontAwesomeIcon
            className="text-orange-500 p-2 -translate-y-1/2 -left-3 absolute rounded-full bg-white border-orange-500 border"
            icon={faArrowLeft}
          />
        </button>
        <button
          onClick={handleNext}
          className="absolute h-full w-1/12 right-20 top-1/2 transform -translate-y-1/2 bg-gradient-to-l from-white/80"
        >
          <FontAwesomeIcon
            className="text-orange-500 p-2 -translate-y-1/2 -right-3 absolute rounded-full bg-white border-orange-500 border"
            icon={faArrowRight}
          />
        </button>
      </div>
    </div>
  );
};

export default AboutUs;
