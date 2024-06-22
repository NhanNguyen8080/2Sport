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
    specialisations: "CEO",
    image: "/assets/images/aboutus/nhan-cu-li.jpg",
  },
  {
    name: "Nguyễn Tuấn Vũ",
    specialisations: "CTO",
    image: "/assets/images/aboutus/sep-tong.jpg",
  },
  {
    name: "Dương Thị Trúc Linh",
    specialisations: "CHRO",
    image: "/assets/images/aboutus/shark-linh.jpg",
  },
  {
    name: "Hà Thị Phương Thảo",
    specialisations: "CIO",
    image: "/assets/images/aboutus/sep-thao.jpg",
  },
  {
    name: "Võ Nguyễn Hoàng Quân",
    specialisations: "CFO",
    image: "/assets/images/aboutus/sep-quan.jpg",
  },
  {
    name: "Võ Hoài Nhật",
    specialisations: "CCO",
    image: "/assets/images/aboutus/sep-nhat.jpg",
  },
  {
    name: "Hà Anh Tài",
    specialisations: "CMO",
    image: "/assets/images/aboutus/sep-tai.jpg",
  },
  {
    name: "Lê Thanh Minh Nhật",
    specialisations: "Design Manager",
    image: "/assets/images/aboutus/nhat-gd.jpg",
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
        <h1 className="text-[40px] py-10 text-white font-rubikmonoone font-bold absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 text-wrap text-center transition duration-300 ease-in-out hover:scale-110">
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
              2Sport luôn là địa chỉ cung cấp các sản phẩm đồ cũ hot,
              trong đó có những thương hiệu hàng đầu thế giới như Yonex, Nike, Adidas,
              Jordan, Jogarbola... với giá rẻ, phù hợp với túi tiền của bạn. TRONG
              Ngoài ra, 2Sport còn sẵn sàng mua bán, ký gửi có hạn
              sản phẩm phiên bản dành cho người sưu tập.
            </p>
          </div>
          <div className="bg-[#EEEEEE] p-[10%]">
            <h1 className="text-[35px] font-bold text-black">GIÁ TRỊ CỐT LÕI:</h1>
            <p className="text-[15px] text-[#6A6A6A] mt-3 mb-5">
              2Sport cam kết mang đến sự tiện lợi và tiết kiệm chi phí
              cho những người yêu thích thể thao, bằng cách cung cấp một nền tảng đáng tin cậy để mua sắm
              đồ thể thao cũ chất lượng và có giá trị. Với cái tên 2-môn thể thao,
              một thương hiệu mới nhưng có tham vọng lớn là mang đến cho khách hàng những sản phẩm
              dịch vụ và trải nghiệm tốt nhất có thể, với phương châm tạo ra
              tiện lợi và tiết kiệm chi phí một cách hiệu quả. hiệu quả cho khách hàng
              và tạo sự tin cậy tối đa để tăng tỷ lệ khách hàng
              quay lại cửa hàng lần sau.
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
              <h2 className="text-2xl font-bold">Sứ mệnh</h2>
              <p className="text-lg mt-3 text-left">
                "2SPORT cam kết mang lại những sản phẩm chất lượng tốt nhất và
                dịch vụ cho người chơi thể thao để cải thiện sức khỏe của chính họ."
              </p>
            </div>
          </div>
          <div className="bg-[#B8DDEB] p-[10%]">
            <h1 className="font-bold text-[25px] text-black">TẦM NHÌN</h1>
            <p className="text-[15px] text-black mt-2">
              <FontAwesomeIcon
                icon={faCircle}
                className="text-black text-[10px] mr-1"
              />
              “Trở thành nhà bán lẻ đồ thể thao cũ lớn nhất Việt Nam”.
            </p>
            <p className="text-[15px] text-black mt-2">
              <FontAwesomeIcon
                icon={faCircle}
                className="text-black text-[10px] mr-1"
              />
              Trở thành đơn vị hàng đầu cả nước trong lĩnh vực đồ cũ
              bán lẻ quần áo thể thao, giúp cải thiện sức khoẻ cộng đồng và bảo vệ
              môi trường.
            </p>
            <p className="text-[15px] text-black mt-2">
              <FontAwesomeIcon
                icon={faCircle}
                className="text-black text-[10px] mr-1"
              />
              Mang đến những sản phẩm chất lượng cao, phù hợp với nhu cầu của
              Con người Việt Nam.
            </p>
            <p className="text-[15px] text-black mt-2 mb-3">
              <FontAwesomeIcon
                icon={faCircle}
                className="text-black text-[10px] mr-1"
              />
              Xây dựng hệ thống cửa hàng rộng khắp 64 tỉnh thành.
            </p>
          </div>
        </div>
      </div>

      <div className="flex flex-col justify-center items-center text-2xl font-bold mt-20">
        <h2 className="text-[#524FF5] justify-center font-bold"></h2>
        <h1 className="text-[35px] font-bold text-black mt-5 mb-5">
          THÀNH VIÊN:
        </h1>
      </div>
      <div className="relative px-20">
        <div className="overflow-hidden">
          <div
            className="flex transition-transform duration-500 ease-in-out"
            style={{ transform: `translateX(-${currentIndex * 33.33}%)` }}
          >
            {members.map((member, index) => (
              <div key={index} className="w-full md:w-1/3 p-2 flex-shrink-0">
                <div className="relative w-full h-60 md:h-72 lg:h-80 bg-gray-200">
                  <img
                    src={member.image}
                    alt={member.name}
                    className="w-full h-full object-contain" // Ensuring the image fits within the container without being cropped
                  />
                </div>
                <div className="bg-[#1B2129] flex flex-col justify-between px-5 py-5 mt-2">
                  <div className="text-xl font-thin justify-between flex mt-5">
                    <h2 className="text-white">{member.name}</h2>
                    <div className="flex items-center ml-auto">
                      {/* Optional additional elements */}
                    </div>
                  </div>
                  <div className="h-px bg-white mt-5"></div>
                  <h2 className="text-white font-thin mt-5 text-xl">
                    {/* Optional additional title or role here */}
                  </h2>
                  <p className="text-[#6A6A6A] text-lg font-thin">
                    {member.specialisations}
                  </p>
                  <div className="flex space-x-3 my-5">
                    <FontAwesomeIcon
                      icon={faInstagram}
                      className="text-white"
                    />
                    <FontAwesomeIcon
                      icon={faFacebook}
                      className="text-white"
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
